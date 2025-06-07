using UnityEngine;
using UnityEngine.Video;
using UnityEngine.InputSystem;
using System.Collections;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject closeHintText;
    public VideoPlayer videoPlayer;
    public float delayToAllowClose = 3f;

    private bool tutorialActive = false;
    private bool canClose = false;
    private Collider triggerCollider;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        tutorialPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !tutorialActive)
        {
            ShowTutorial();
        }
    }

    void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
        videoPlayer.Play();
        tutorialActive = true;
        canClose = false;

        Time.timeScale = 0f; // Pausar todo el juego
        StartCoroutine(EnableCloseDelayed());
        
        triggerCollider.enabled = false; // Evita que vuelva a activarse
    }

    IEnumerator EnableCloseDelayed()
    {
        yield return new WaitForSecondsRealtime(delayToAllowClose);
        canClose = true;
        closeHintText.SetActive(true);
    }


    void Update()
    {
        if (tutorialActive && canClose && Mouse.current.leftButton.wasPressedThisFrame)
        {
            CloseTutorial();
        }
    }

    void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        videoPlayer.Stop();
        tutorialActive = false;
        canClose = false;

        Time.timeScale = 1f; 
    }
}