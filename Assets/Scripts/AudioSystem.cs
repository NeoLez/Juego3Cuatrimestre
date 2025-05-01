using UnityEngine;

public class AudioSystem {
    public AudioSource NonPositionAudioSource;

    public void PlaySound(AudioClip audioClip) {
        NonPositionAudioSource?.PlayOneShot(audioClip);
    }
}