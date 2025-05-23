﻿using UnityEngine;
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
    [SerializeField] private float rotationSensitivity;
    [SerializeField] private float torqueStrength = 10f;
    [SerializeField] private float angularDamping = 2f;

    private bool currentlyRotating;
    private bool currentlyDragging;
    
    private void Awake() {
        GameManager.Input.CameraMovement.DragObject.started += _ => {
            if (currentlyDragging) StopDrag();
            else StartDrag();
        };
        GameManager.Input.Drag.DragRotate.started += _ => {
            if (currentlyDragging) {
                currentlyRotating = true;
                GameManager.Input.CameraMovement.Disable();
            }
        };
        GameManager.Input.Drag.DragRotate.canceled += _ => {
            currentlyRotating = false;
            if (currentlyDragging) {
                GameManager.Input.CameraMovement.Enable();
            }
        };
    }

    private void Update() {
        if (obj == null) {
            currentlyDragging = false;
            return;
        }
        
        if (currentlyDragging) {
            if((!GameManager.Input.CameraMovement.enabled && !currentlyRotating) || obj.gameObject.layer != LayerMask.NameToLayer("DraggableObject")) {
                StopDrag();
                return;
            }
                
            if (Vector3.Distance(GameManager.MainCamera.transform.position, obj.transform.position) > maxDistance ||
                Vector3.Angle(obj.transform.position - GameManager.MainCamera.transform.position, GameManager.MainCamera.transform.forward) > maxAngle) {
                StopDrag();
                return;
            }

            if (GameManager.Input.Drag.DragRotate.IsPressed()) {
                float x = GameManager.Input.Drag.MouseX.ReadValue<float>() * rotationSensitivity;
                float y = GameManager.Input.Drag.MouseY.ReadValue<float>() * rotationSensitivity;
                dragRotOffset = Quaternion.Euler(y, -x, 0) * dragRotOffset;
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