using UnityEngine;

public class ManagerPlayerRegistration : MonoBehaviour{
    private void Awake() {
        GameManager.Player = gameObject;
        GameManager.Input = new PlayerInputActions();
        GameManager.Input.Enable();
        GameManager.Input.Movement.Enable();
    }
}