using UnityEngine;

public class AudioSystem {
    public AudioSource NonPositionAudioSource;

    public void PlaySound(AudioClip audioClip, float volume = 1) {
        NonPositionAudioSource?.PlayOneShot(audioClip, volume);
    }

    public void PlaySoundPositional(AudioClip audioClip, Vector3 position, float volume = 1) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}