using UnityEngine;
using UnityEngine.InputSystem;

public class Drag : MonoBehaviour {
    [SerializeField] private Rigidbody obj;
    [SerializeField] private float rayDistance;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    private float currentDistance;
    [SerializeField] private float maxAngle;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float maxSpeed;
    private Quaternion dragRotOffset;
    [SerializeField] private float torqueStrength = 10f;
    [SerializeField] private float angularDamping = 2f;

    private bool currentlyDragging;
    
    private void Awake() {
        GameManager.Input.CameraMovement.DragObject.started += _ => {
            if (currentlyDragging) StopDrag();
            else StartDrag();
        };
    }

    private void Update() {
        if (obj != null && currentlyDragging) {
            if(!GameManager.Input.CameraMovement.enabled || obj.gameObject.layer != LayerMask.NameToLayer("DraggableObject")) {
                StopDrag();
                return;
            }
                
            if (Vector3.Distance(GameManager.MainCamera.transform.position, obj.transform.position) > maxDistance ||
                Vector3.Angle(obj.transform.position - GameManager.MainCamera.transform.position, GameManager.MainCamera.transform.forward) > maxAngle) {
                StopDrag();
                return;
            }
            Vector3 target = GameManager.MainCamera.transform.position + GameManager.MainCamera.transform.forward * currentDistance;
            Vector3 speed = (target - obj.transform.position) * speedMultiplier;
            if (speed.magnitude > maxSpeed) {
                speed.Normalize();
                speed *= maxSpeed;
            }
            obj.velocity = speed;
            
            Quaternion desiredRot = GameManager.MainCamera.transform.rotation * dragRotOffset;
            Quaternion deltaRot = desiredRot * Quaternion.Inverse(obj.rotation);
            deltaRot.ToAngleAxis(out float angleDeg, out Vector3 axis);
            if (angleDeg > 180f) angleDeg -= 360f;
            // avoid NaNs when angle is very small
            if (axis.sqrMagnitude > 0.001f) {
                // torque = k_p * angle * axis  –  k_d * angularVelocity
                Vector3 correctiveTorque = axis.normalized * (angleDeg * Mathf.Deg2Rad * torqueStrength)
                                           - obj.angularVelocity * angularDamping;
                obj.AddTorque(correctiveTorque, ForceMode.Acceleration);
            }
        }
    }

    private void StartDrag() {
        Ray ray = new Ray(GameManager.MainCamera.transform.position, GameManager.MainCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, LayerMask.GetMask("Ground"))) {
            if (hit.collider.gameObject.TryGetComponent(out ObjectStatus objectStatus)) {
                if (objectStatus.Type == ObjectTypeEnum.PhysicsObject) {
                    hit.collider.gameObject.layer = LayerMask.NameToLayer("DraggableObject");
                    obj = hit.rigidbody;
                    currentDistance = Vector3.Distance(obj.position, GameManager.MainCamera.transform.position);
                    if (currentDistance < minDistance)
                        currentDistance = minDistance;
            
                    dragRotOffset = Quaternion.Inverse(GameManager.MainCamera.transform.rotation)
                                    * obj.transform.rotation;
                    currentlyDragging = true;
                }
            }
        }
    }

    public void DisengageObject(GameObject gameObject) {
        if (obj != null && gameObject == obj.gameObject) {
            StopDrag();
        }
    }

    private void StopDrag() {
        currentlyDragging = false;
        obj.gameObject.layer = LayerMask.NameToLayer("Ground");
        obj = null;
    }
}