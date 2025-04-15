using System;
using UnityEngine;
using UnityEngine.InputSystem;
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

        _input.Movement.Jump.started += InputJumpStarted;
        _input.Movement.Jump.canceled += InputJumpEnded;
    }


    [SerializeField] private float jumpInputBufferTime;
    private bool _jumpInputRegistered;
    private float _jumpInputStartTime;
    private bool _currentlyJumping;
    private float _jumpStartTime;
    private byte _currentJumps;
    [SerializeField] private byte maxJumps;
    [SerializeField] private float maxJumpDuration;
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private AnimationCurve jumpCurve;
    private void InputJumpStarted(InputAction.CallbackContext ctx) {
        _jumpInputRegistered = true;
        _jumpInputStartTime = Time.fixedTime;
    }
    private void InputJumpEnded(InputAction.CallbackContext ctx) {
        _jumpInputRegistered = false;
    }
    private bool ShouldStartJump() {
        return _jumpInputRegistered && Time.fixedTime - _jumpInputStartTime <= jumpInputBufferTime;
    }

    private Vector2 _moveDir = Vector2.zero;
    void Update() {
        _moveDir = _input.Movement.MoveDir.ReadValue<Vector2>();
    }

    private Vector3 _prevSpeed = Vector3.zero;
    private Vector3 _prevGravity = Vector3.zero;
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
            v = Vector3.Lerp(_prevSpeed, v, airLerp);
            _prevSpeed = v;
            gravity = _prevGravity + Physics.gravity * Time.fixedDeltaTime;
            _prevGravity = gravity;
        }
        else if (state == CharacterState.Grounded) {
            _currentJumps = maxJumps;
            
            v = Vector3.ProjectOnPlane(v, _surfaceNormal);
            //v *= angleBasedSpeedLimit.Evaluate(currentSlopeAngle / 90f);
            v = Vector3.Lerp(_prevSpeed, v, groundLerp);
            _prevGravity = Vector3.zero;
            _prevSpeed = v;
        }else if (state == CharacterState.Sliding) {
            v = Vector3.ProjectOnPlane(v, _surfaceNormal);
            if (v.y > 0)
                v.y = 0;
            
            v = Vector3.Lerp(_prevSpeed, v, groundLerp);
            
            gravity = _prevGravity + Vector3.ProjectOnPlane(Physics.gravity * Time.fixedDeltaTime, _surfaceNormal);
            Vector3 gravityClone = gravity.Swizzle_xyz();
            gravityClone.y = 0;
            v += gravityClone;
            _prevSpeed = v;
            _prevGravity = gravity;
        }
        
        //TODO Jumping stops too suddenly. Maybe switch from curves to a more standard set speed + extra force when jump is held.
        if (!_currentlyJumping && ShouldStartJump() && _currentJumps > 0 && _currentDashTime > _dashTime) {
            _currentlyJumping = true;
            _jumpStartTime = Time.fixedTime;
            _currentJumps--;
        }
        if (_currentlyJumping) {
            if (!_jumpInputRegistered) {
                _currentlyJumping = false;
                gravity = Vector3.zero;
                _prevGravity = gravity;
            }
            else {
                float percentage = (Time.fixedTime - _jumpStartTime) / maxJumpDuration;
                if (percentage > 1.0) {
                    _currentlyJumping = false;
                }
                else {
                    float a1 = jumpCurve.Evaluate(percentage);
                    float a2 = jumpCurve.Evaluate(percentage + 0.001f);
                    float speed = (((a2 - a1) * maxJumpHeight) / 0.001f) / maxJumpDuration;

                    gravity = Vector3.up * speed;
                    _prevGravity = gravity;
                }
            }
        }
        
        _rb.velocity = v + gravity;
        
        Debug.DrawRay(_rb.position + Vector3.down, v, Color.red);
        
        DoDash();
    }


    #region Dash
    private Vector3 _dashVector;
    private float _dashTime;
    private float _dashDistance;
    private float _currentDashTime = Int32.MaxValue;
    private AnimationCurve _dashCurve;

    private void DoDash() {
        if (_currentDashTime < _dashTime) {
            _prevGravity = Vector3.zero;
            _currentDashTime += Time.fixedDeltaTime;
            float a1 = _dashCurve.Evaluate(_currentDashTime/_dashTime);
            float a2 = _dashCurve.Evaluate((_currentDashTime / _dashTime) + 0.001f);
            float speed = (((a2 - a1) * _dashDistance) / 0.001f) / _dashTime;
            
            _rb.velocity = _dashVector * speed;
        }
    }
    public void Dash(Vector3 moveVector, float distance, float time, AnimationCurve curve) {
        _dashTime = time;
        _dashCurve = curve;
        _dashDistance = distance;
        _currentDashTime = 0;
        _dashVector = moveVector.normalized;
    }
    #endregion
}

public enum CharacterState {
    Grounded,
    Sliding,
    Air,
}
