using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private Transform cam;
    [SerializeField] private float sensitivity = 1;

    [SerializeField] private float sideSwayAngle;
    [SerializeField] private float swaySpeed;
    [SerializeField] private float horizontalAmount;
    [SerializeField] private float verticalAmount;
    [SerializeField] private float frequency;
    [SerializeField] private float smooth;
    private Vector2 cameraBobbingOffset;
    private float currentSideSwayAngle;
    
    [SerializeField] private float yaw;
    [SerializeField] private float pitch;
    
    private PlayerInputActions _input;
    private MovementControllerTest _movementController;

    private void Start() {
        _input = GameManager.Input;
        _movementController = GameManager.Player.GetComponent<MovementControllerTest>();
        cam = GameManager.MainCamera.transform;
        
        
        LockCamera();
    }

    private void Update() {
        Vector2 moveDir = _input.Movement.MoveDir.ReadValue<Vector2>();
        
        if (moveDir.magnitude > 0 && _movementController.GetState() != CharacterState.Air) {
            HeadBob();
        }
        else {
            cameraBobbingOffset = Vector2.Lerp(cameraBobbingOffset, Vector2.zero, smooth * Time.deltaTime);
        }

        Vector3 viewBobVector = GetHorizontalDirectionRightVector().Swizzle_x0y() * cameraBobbingOffset.x + Vector3.up * cameraBobbingOffset.y;
        cam.position = cameraPosition.position + viewBobVector;
        
        yaw += _input.CameraMovement.MouseX.ReadValue<float>() * sensitivity;
        pitch += _input.CameraMovement.MouseY.ReadValue<float>() * sensitivity;

        pitch = Mathf.Clamp(pitch, -89f, 89f);
        if (yaw > 360)
            yaw -= 360;
        else if (yaw < 0)
            yaw += 360;
        
        
        float target = -moveDir.x * sideSwayAngle;
        currentSideSwayAngle = (target - currentSideSwayAngle) * swaySpeed + currentSideSwayAngle;
        cam.localRotation = Quaternion.Euler(-pitch, yaw, currentSideSwayAngle);
    }

    private void HeadBob() {
        cameraBobbingOffset.y = Mathf.Lerp(cameraBobbingOffset.y, Mathf.Sin(Time.time * frequency) * verticalAmount * 1.4f,
            smooth * Time.deltaTime);
        cameraBobbingOffset.x = Mathf.Lerp(cameraBobbingOffset.x, Mathf.Cos(Time.time * frequency / 2) * horizontalAmount * 1.6f,
            smooth * Time.deltaTime);
    }

    public void LockCamera() {
        Cursor.visible = false;
        _input.CameraMovement.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCamera() {
        Cursor.visible = true;
        _input.CameraMovement.Disable();
        Cursor.lockState = CursorLockMode.Confined;
    }

    public Vector2 GetHorizontalDirectionForwardVector() {
        return new Vector2(Mathf.Sin(yaw * Mathf.Deg2Rad), Mathf.Cos(yaw * Mathf.Deg2Rad));
    }
    public Vector2 GetHorizontalDirectionRightVector() {
        return new Vector2(Mathf.Cos(yaw * Mathf.Deg2Rad), -Mathf.Sin(yaw * Mathf.Deg2Rad));
    }
}
