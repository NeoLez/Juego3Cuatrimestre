using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CameraController))]
public class MobileDrawingSurface : MonoBehaviour {
    [SerializeField] private GameObject surface;
    [SerializeField] private float distance;
    private PlayerInputActions _input;
    private CameraController _cameraController;
    private bool _surfaceActive;

    private void Awake() {
        _input = new PlayerInputActions();
        _input.Enable();
        _input.Movement.Enable();

        _cameraController = GetComponent<CameraController>();
        
        surface.SetActive(_surfaceActive);

        _input.Movement.OpenBook.started += OpenBook;
    }

    private void LateUpdate() {
        if (_surfaceActive) {
            Camera camera = Camera.main;
            surface.transform.position = camera.transform.position + camera.transform.forward * distance;
            surface.transform.forward = camera.transform.forward;
        }
    }

    private void OpenBook(InputAction.CallbackContext ctx) {
        if (_surfaceActive) {
            _surfaceActive = false;
            surface.SetActive(false);
            _cameraController.LockCamera();
        }
        else {
            _surfaceActive = true;
            surface.SetActive(true);
            _cameraController.UnlockCamera();
        }
    }
}