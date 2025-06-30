using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public GameObject PauseMenu;
    public bool paused = false;
    public GameObject SalirConfirmar;
    public GameObject OpcionConfirmar;

    public Slider volumenSlider;

    [Header("Sonidos")]
    public AudioClip sonidoPausa;
    public AudioClip sonidoReanudar;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        volumenSlider.value = AudioListener.volume;
        volumenSlider.onValueChanged.AddListener(CambiarVolumen);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                if (sonidoPausa != null) audioSource.PlayOneShot(sonidoPausa);

                PauseMenu.SetActive(true);
                paused = true;

                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                GameManager.Input.Movement.Disable();
                GameManager.Input.CameraMovement.Disable();
                GameManager.Input.BookActions.Disable();
                GameManager.Input.Scanner.Disable();
                GameManager.Input.Drag.Disable();
                GameManager.Input.CardUsage.Disable();

                AudioSource[] songs = FindObjectsOfType<AudioSource>();
                foreach (AudioSource s in songs)
                {
                    if (s != audioSource)
                    {
                        s.Pause();
                    }
                }
            }
            else
            {
                if (sonidoReanudar != null) audioSource.PlayOneShot(sonidoReanudar);
                resume();
            }
        }
    }

    public void resume()
    {
        PauseMenu.SetActive(false);
        SalirConfirmar.SetActive(false);
        OpcionConfirmar.SetActive(false);
        paused = false;
        

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        GameManager.Input.Movement.Enable();
        GameManager.Input.CameraMovement.Enable();
        GameManager.Input.BookActions.Enable();
        GameManager.Input.Scanner.Enable();
        GameManager.Input.Drag.Enable();
        GameManager.Input.CardUsage.Enable();
        
        AudioSource[] songs = FindObjectsOfType<AudioSource>();
        foreach (AudioSource s in songs)
        {
            if (s != audioSource)
            {
                s.Play();
            }
        }
    }

    public void CambiarVolumen(float volumen)
    {
        AudioListener.volume = volumen;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("saliste xd");
    }
}
