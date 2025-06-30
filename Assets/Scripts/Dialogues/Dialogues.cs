using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogues : MonoBehaviour
{
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private float typingSpeed = 0.5f;
    [SerializeField] private bool shouldStartAuto = false;
    [SerializeField] private GameObject dialogueInteraction;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(3, 5)] private string[] dialogueLines;
    
    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
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
    }

    private void StartDialogue()
    {
       didDialogueStart = true;
       dialoguePanel.SetActive(true);
       dialogueInteraction.SetActive(false);
       lineIndex = 0;
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
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueInteraction.SetActive(true);
        }
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
            
            if(shouldStartAuto)
            {
                if (!didDialogueStart) 
                {
                    StartDialogue();
                    NextDialogueLine();
                }
            }
            else
            {
                dialogueInteraction.SetActive(true);
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
