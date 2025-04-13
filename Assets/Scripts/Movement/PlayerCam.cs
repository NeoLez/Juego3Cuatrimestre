using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    private PlayerInputActions _input;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _input = new PlayerInputActions();
        _input.Enable();
        _input.Movement.Enable();
    }

    private void Update()
    {
        float mouseX = _input.Movement.MouseX.ReadValue<float>() * Time.deltaTime * sensX;
        float mouseY = _input.Movement.MouseY.ReadValue<float>() * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void DoFov(float endValue)
    {
        //GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }
}
