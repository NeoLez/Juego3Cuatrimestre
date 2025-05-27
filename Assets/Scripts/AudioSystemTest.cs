using UnityEngine;

namespace DefaultNamespace {
    public class AudioSystemTest : MonoBehaviour {
        [SerializeField] private AudioClip AudioClip;
        [SerializeField] private Vector3 Position;
        [SerializeField] private bool play;
        [SerializeField] private bool positional;

        private void Update() {
            if(!play) return;

            play = false;

            if (positional) {
                GameManager.AudioSystem.PlaySoundPositional(AudioClip, Position);
            }
            else {
                GameManager.AudioSystem.PlaySound(AudioClip);
            }
        }
    }
}