using System.Collections.Generic;
using UnityEngine;

public class AudioSystem {
    public AudioSource NonPositionAudioSource;
    private List<AudioSource> currentlyLoopingSounds = new();

    public void PlaySound(AudioClip audioClip, float volume = 1) {
        NonPositionAudioSource?.PlayOneShot(audioClip, volume);
    }

    public void PlaySoundPositional(AudioClip audioClip, Vector3 position, float volume = 1) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public AudioSource PlaySoundLooping(AudioClip audioClip, Vector3 position, float volume = 1) {
        GameObject go = new GameObject();
        go.transform.position = position;
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.clip = audioClip;
        audioSource.Play();

        return audioSource;
    }
    
    public AudioSource PlaySoundLooping(AudioClip audioClip, float volume = 1) {
        GameObject go = new GameObject();
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.clip = audioClip;
        audioSource.Play();

        return audioSource;
    }
}