using UnityEngine;
using UnityEngine.InputSystem;

public class Drag : MonoBehaviour {
    [SerializeField] private Rigidbody obj;
    [SerializeField] private float distance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float maxAngle;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float maxSpeed;
    private Quaternion dragRotOffset;
    [SerializeField] private float torqueStrength = 10f;
    [SerializeField] private float angularDamping = 2f;

    private void Awake() {
        GameManager.Input.CameraMovement.DragObject.started += StartDrag;
        GameManager.Input.CameraMovement.DragObject.canceled += StopDrag;
    }

    private void Update() {
        if (obj != null) {
            if (Vector3.Distance(GameManager.MainCamera.transform.position, obj.transform.position) > maxDistance ||
                Vector3.Angle(obj.transform.position - GameManager.MainCamera.transform.position, GameManager.MainCamera.transform.forward) > maxAngle) {
                obj = null;
                return;
            }
            Vector3 target = transform.position + GameManager.MainCamera.transform.forward * distance;
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

    private void StartDrag(InputAction.CallbackContext ctx) {
        Ray ray = new Ray(GameManager.MainCamera.transform.position, GameManager.MainCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, distance, LayerMask.NameToLayer("DraggableObject"))) {
            obj = hit.rigidbody;
            dragRotOffset = Quaternion.Inverse(GameManager.MainCamera.transform.rotation)
                            * obj.transform.rotation;
        }
    }

    private void StopDrag(InputAction.CallbackContext ctx) {
        obj = null;
    }
}