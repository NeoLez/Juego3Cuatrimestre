using UnityEngine;

public class ManagerPlayerRegistration : MonoBehaviour{
    private void Awake() {
        GameManager.Player = gameObject;
    }
}