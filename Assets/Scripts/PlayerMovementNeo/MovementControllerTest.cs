using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody), typeof(CameraController))]
public class MovementControllerTest : MonoBehaviour {
    private PlayerInputActions _input;
    private Rigidbody _rb;
    private CameraController _cameraController;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float groundCheckRayLength;
    [SerializeField] private LayerMask layer;

    [SerializeField] private float airLerp;
    [SerializeField] private float groundLerp;

    [SerializeField] private AnimationCurve angleBasedSpeedLimit;
    
    [SerializeField] private float maxSlopeAngle;
    [SerializeField] private float currentSlopeAngle;
    [SerializeField] private CharacterState state = CharacterState.Grounded;
    private Vector3 _surfaceNormal;
    
    void Start() {
        _input = new();
        _input.Enable();
        _input.Movement.Enable();

        _rb = GetComponent<Rigidbody>();
        _cameraController = GetComponent<CameraController>();
    }

    private Vector2 _moveDir = Vector2.zero;
    void Update() {
        _moveDir = _input.Movement.MoveDir.ReadValue<Vector2>();
    }

    private Vector3 prevSpeed = Vector3.zero;
    private Vector3 prevGravity = Vector3.zero;
    private void FixedUpdate() {
        Vector3 worldMoveDir = (_cameraController.GetHorizontalDirectionForwardVector() * _moveDir.y +
                     _cameraController.GetHorizontalDirectionRightVector() * _moveDir.x).Swizzle_x0y();
        
        if (Physics.SphereCast(_rb.position, 0.499f, Vector3.down, out RaycastHit hit, groundCheckRayLength, layer)) {
            if (Physics.Raycast(hit.point + Vector3.up, Vector3.down, out hit, 2f, layer)) {
                currentSlopeAngle = Vector3.Angle(hit.normal, Vector3.up);
                if(currentSlopeAngle <= maxSlopeAngle) {
                    state = CharacterState.Grounded;
                    _surfaceNormal = hit.normal;
                }
                else
                {
                    _surfaceNormal = hit.normal;
                    state = CharacterState.Sliding;
                }
            }
        }
        else {
            state = CharacterState.Air;
        }
        
        Vector3 v = worldMoveDir * movementSpeed;
        Vector3 gravity = Vector3.zero;
        if (state == CharacterState.Air) {
            v = Vector3.Lerp(prevSpeed, v, airLerp);
            prevSpeed = v;
            gravity = prevGravity + Physics.gravity * Time.fixedDeltaTime;
            prevGravity = gravity;
        }
        else if (state == CharacterState.Grounded) {
            v = Vector3.ProjectOnPlane(v, _surfaceNormal);
            //v *= angleBasedSpeedLimit.Evaluate(currentSlopeAngle / 90f);
            v = Vector3.Lerp(prevSpeed, v, groundLerp);
            prevGravity = Vector3.zero;
            prevSpeed = v;
        }else if (state == CharacterState.Sliding) {
            v = Vector3.ProjectOnPlane(v, _surfaceNormal);
            if (v.y > 0)
                v.y = 0;
            
            v = Vector3.Lerp(prevSpeed, v, groundLerp);
            
            gravity = prevGravity + Vector3.ProjectOnPlane(Physics.gravity * Time.fixedDeltaTime, _surfaceNormal);
            Vector3 gravityClone = gravity.Swizzle_xyz();
            gravityClone.y = 0;
            v += gravityClone;
            prevSpeed = v;
            prevGravity = gravity;
        }
        _rb.velocity = v + gravity;
        Debug.DrawRay(_rb.position + Vector3.down, v, Color.red);
        
        DoDash();
    }
    
    private Vector3 dashVector;
    private float time;
    private float distance;
    private float currentTime = Int32.MaxValue;
    private AnimationCurve curve;

    private void DoDash() {
        if (currentTime < time) {
            currentTime += Time.fixedDeltaTime;
            float a1 = curve.Evaluate(currentTime/time);
            float a2 = curve.Evaluate((currentTime / time) + 0.001f);
            float speed = (((a2 - a1) * distance) / 0.001f) / time;
            
            _rb.velocity = dashVector * speed;
        }
    }
    public void Dash(Vector3 moveVector, float distance, float time, AnimationCurve curve) {
        this.time = time;
        this.curve = curve;
        this.distance = distance;
        currentTime = 0;
        dashVector = moveVector.normalized;
    }
}

public enum CharacterState {
    Grounded,
    Sliding,
    Air,
}
