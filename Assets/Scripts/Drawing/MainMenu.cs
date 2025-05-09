using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    [SerializeField] private GameObject controles;

    public void ShowControles() {
        controles.SetActive(true);
    }
    public void HideControles() {
        controles.SetActive(false);
    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Cerraste el juego");
    }
}
