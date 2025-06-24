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

    void Start()
    {
        volumenSlider.value = AudioListener.volume;
        volumenSlider.onValueChanged.AddListener(CambiarVolumen);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                PauseMenu.SetActive(true);
                paused = true;

                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                GameManager.Input.Movement.Disable();
                GameManager.Input.CameraMovement.Disable();

                AudioSource[] songs = FindObjectsOfType<AudioSource>();
                for (int i = 0; i < songs.Length; i++)
                {
                    songs[i].Pause();
                }
            }
            else
            {
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

        AudioSource[] songs = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < songs.Length; i++)
        {
            songs[i].Play();
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
