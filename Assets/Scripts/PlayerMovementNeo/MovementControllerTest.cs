using UnityEngine;

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
    [SerializeField] private CharacterState state = CharacterState.Grounded;
    
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
    
    private void FixedUpdate() {
        Vector3 v = (_cameraController.GetHorizontalDirectionForwardVector() * _moveDir.y +
                     _cameraController.GetHorizontalDirectionRightVector() * _moveDir.x).Swizzle_x0y();
        
        if (Physics.SphereCast(_rb.position, 0.499f, Vector3.down, out RaycastHit hit, groundCheckRayLength, layer)) {
            if (Physics.Raycast(hit.point + Vector3.up, Vector3.down, out hit, 2f, layer)) {
                float angle = Vector3.Angle(hit.normal, Vector3.up);
                if(angle <= maxSlopeAngle) {
                    state = CharacterState.Grounded;
                    v -= Vector3.Dot(v, hit.normal) * hit.normal;
                    v *= angleBasedSpeedLimit.Evaluate(angle / 90f);
                    Debug.Log(angleBasedSpeedLimit.Evaluate(angle / 90f));
                    Debug.Log(hit.collider.name);
                }
            }
        }
        else {
            state = CharacterState.Air;
        }
        Debug.Log(Vector3.Angle(hit.normal, Vector3.up));
        Debug.DrawRay(_rb.position + Vector3.down, v, Color.red);
        
        v *= movementSpeed;
        
        if (state == CharacterState.Air) {
            v = Vector3.Lerp(_rb.velocity.Swizzle_x0z(), v, airLerp);
            v.y = _rb.velocity.y + Physics.gravity.y * Time.fixedDeltaTime;
            _rb.velocity = v;
        }
        else {
            v = Vector3.Lerp(_rb.velocity.Swizzle_x0z(), v, groundLerp);
            _rb.velocity = v;
        }
    }
}

public enum CharacterState {
    Grounded,
    Air,
}
