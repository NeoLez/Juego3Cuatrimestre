using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private Transform cam;
    [SerializeField] private float sensitivity = 1;

    [SerializeField] private float yaw;
    [SerializeField] private float pitch;
    
    private PlayerInputActions _input;

    private void Awake() {
        _input = GameManager.Input;
        cam = Camera.main.transform;
        
        LockCamera();
    }

    private void Update() {
        cam.position = cameraPosition.position;
        
        yaw += _input.CameraMovement.MouseX.ReadValue<float>() * sensitivity;
        pitch += _input.CameraMovement.MouseY.ReadValue<float>() * sensitivity;

        pitch = Mathf.Clamp(pitch, -89f, 89f);
        if (yaw > 360)
            yaw -= 360;
        else if (yaw < 0)
            yaw += 360;
        
        cam.localRotation = Quaternion.Euler(-pitch, yaw, 0);
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
