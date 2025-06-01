using UnityEngine;

public class ManagerPlayerRegistration : MonoBehaviour{
    private void Awake() {
        GameManager.Player = gameObject;
        GameManager.MainCamera = Camera.main;
        GameManager.CamFOV = GameManager.MainCamera.fieldOfView;
    }
}