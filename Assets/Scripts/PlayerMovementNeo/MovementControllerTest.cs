using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CameraController))]
public class MovementControllerTest : MonoBehaviour {
    private PlayerInputActions _input;
    private Rigidbody _rb;
    private CameraController _cameraController;

    [SerializeField] private float movementSpeed;
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
        v *= movementSpeed;
        
        v.y = _rb.velocity.y;
        _rb.velocity = v;
    }
}
