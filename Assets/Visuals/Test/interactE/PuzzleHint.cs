using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class PuzzleHint : MonoBehaviour
{
    public string hintMessage;
    public GameObject worldHintTextObject; //en este de aca pongan el canvas :p 
    public TextMeshProUGUI worldHintText; //en este de aca pongan el texto que crean como hijo del canvas :p
    public float hintDuration = 5f;

    private bool isPlayerInRange;
    private Coroutine hintCoroutine;

    void Start()
    {
        if (worldHintTextObject != null)
            worldHintTextObject.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (hintCoroutine != null)
                StopCoroutine(hintCoroutine);

            hintCoroutine = StartCoroutine(ShowHint());
        }
        
    }

    IEnumerator ShowHint()
    {
        worldHintTextObject.SetActive(true);
        worldHintText.text = hintMessage;

        yield return new WaitForSeconds(hintDuration);

        worldHintTextObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInRange = false;
    }
}
