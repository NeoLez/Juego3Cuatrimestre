using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private Transform cam;
    [SerializeField] private float sensitivity = 1;

    [SerializeField] private float yaw;
    [SerializeField] private float pitch;
    
    private PlayerInputActions input;

    private void Awake() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        input = new();
        input.Enable();
        input.Movement.Enable();

        cam = Camera.main.transform;
    }

    private void Update() {
        cam.position = cameraPosition.position;
        yaw += input.Movement.MouseX.ReadValue<float>() * sensitivity;
        pitch += input.Movement.MouseY.ReadValue<float>() * sensitivity;

        pitch = Mathf.Clamp(pitch, -89f, 89f);
        if (yaw > 360)
            yaw -= 360;
        else if (yaw < 0)
            yaw += 360;
        
        cam.localRotation = Quaternion.Euler(-pitch, yaw, 0);
    }

    public Vector2 GetHorizontalDirectionForwardVector() {
        return new Vector2(Mathf.Sin(yaw * Mathf.Deg2Rad), Mathf.Cos(yaw * Mathf.Deg2Rad));
    }
    public Vector2 GetHorizontalDirectionRightVector() {
        return new Vector2(Mathf.Cos(yaw * Mathf.Deg2Rad), -Mathf.Sin(yaw * Mathf.Deg2Rad));
    }
}
