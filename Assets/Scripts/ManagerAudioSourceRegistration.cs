using UnityEngine;

public class ManagerAudioSourceRegistration : MonoBehaviour {
    private void Awake() {
        GameManager.AudioSystem.NonPositionAudioSource = GetComponent<AudioSource>();
    }
}