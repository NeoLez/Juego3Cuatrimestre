using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OpcionesAudio : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);

        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
    }

    public void SetMusicVolume(float volume)
    {
        if (volume < 0.0001f) volume = 0.0001f; 
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        if (volume < 0.0001f) volume = 0.0001f;  
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

}

