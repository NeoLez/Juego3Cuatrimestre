using UnityEngine;

public class MovementController : MonoBehaviour {
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private float movementStrength;
    [SerializeField] private float maxSpeed;

    private PlayerInputActions input;
    
    private void Awake() {
        input = new();
        input.Enable();
        input.Movement.Enable();
    }

    private void FixedUpdate() {
        Vector2 moveDir = input.Movement.MoveDir.ReadValue<Vector2>();
        Vector2 forceDirection = moveDir.y * cameraController.GetHorizontalDirectionForwardVector() +
                                 moveDir.x * cameraController.GetHorizontalDirectionRightVector();
        
        if(rb.velocity.Swizzle_xz().magnitude < maxSpeed)
            rb.AddForce((forceDirection * (Time.deltaTime * movementStrength)).Swizzle_x0y());
    }
}
