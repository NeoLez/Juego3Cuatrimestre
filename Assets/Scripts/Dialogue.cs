using System.Collections;
using UnityEngine;
using TMPro;

public class AutoDialogueTrigger : MonoBehaviour
{
    [TextArea(3, 5)]
    public string dialogueText;
    public float typingSpeed = 0.05f;
    public float dialogueDuration = 3f;
    public TMP_Text dialogueTMP;
    public GameObject dialoguePanel;

    private bool hasTriggered = false;

    private void Start()
    {
        dialoguePanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;

            dialoguePanel.SetActive(true);
            StartCoroutine(ShowDialogue());
        }
    }

    private IEnumerator ShowDialogue()
    {
        dialogueTMP.text = "";
        foreach (char c in dialogueText)
        {
            dialogueTMP.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(dialogueDuration);
        dialoguePanel.SetActive(false);
        gameObject.SetActive(false); 
    }
}

