using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogues : MonoBehaviour
{
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private float typingSpeed = 0.05f;
    [SerializeField] private bool shouldStartAuto = false;
    [SerializeField] private bool hasAutoPlayed = false;
    [SerializeField] private GameObject dialogueInteraction;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(3, 5)] private string[] dialogueLines;
    
    void Update()
    {
        if (didDialogueStart) return;
        
        if (shouldStartAuto && isPlayerInRange && !hasAutoPlayed)
        {
            GameManager.Input.Movement.Disable();
            GameManager.Input.CameraMovement.Disable();
            GameManager.Input.BookActions.Disable();
            GameManager.Input.Scanner.Disable();
            GameManager.Input.Drag.Disable();
            GameManager.Input.CardUsage.Disable();
            StartDialogue(auto: true);
        }
        else if (!shouldStartAuto && isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue(auto: false);
        }
        else if (dialogueText.text == dialogueLines[lineIndex])
        {
            NextDialogueLine();
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[lineIndex];
        }
    }
    void LateUpdate()
    {
        if (!didDialogueStart) return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueText.text == dialogueLines[lineIndex])
                NextDialogueLine();
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }
    private void StartDialogue(bool auto)
    {
       didDialogueStart = true;
       dialoguePanel.SetActive(true);
       dialogueInteraction.SetActive(false);
       lineIndex = 0;
       if (auto) hasAutoPlayed = false;
           StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            EndDialogue();
            dialoguePanel.SetActive(false);
            dialogueInteraction.SetActive(true);
            GameManager.Input.Movement.Enable();
            GameManager.Input.CameraMovement.Enable();
            GameManager.Input.BookActions.Enable();
            GameManager.Input.Scanner.Enable();
            GameManager.Input.Drag.Enable();
            GameManager.Input.CardUsage.Enable();
        }
    }
    private void EndDialogue()
    {
        didDialogueStart = false;
        dialoguePanel.SetActive(false);
        if (!shouldStartAuto)
        {
            dialogueInteraction.SetActive(true);
        }
        if (shouldStartAuto)
            hasAutoPlayed = true;
    }
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if(!shouldStartAuto)
            {
                dialogueInteraction.SetActive(true);
            }
            else if (!didDialogueStart)
            {
                
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueInteraction.SetActive(false);
        }
    }
}
