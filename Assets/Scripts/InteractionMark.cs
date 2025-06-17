using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMark : MonoBehaviour
{
    [SerializeField] private GameObject interactionMark;
    [SerializeField] private float floatAmplitude = 0.1f; 
    [SerializeField] private float floatFrequency = 1f;  

    private Vector3 originalLocalPosition;

    private void Start()
    {
        if (interactionMark != null)
        {
            interactionMark.SetActive(false);
            originalLocalPosition = interactionMark.transform.localPosition;
        }
    }
    private void Update()
    {
        if (interactionMark != null && interactionMark.activeSelf)
        {
            if (Camera.main != null)
                interactionMark.transform.forward = Camera.main.transform.forward;
            
            float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            interactionMark.transform.localPosition = originalLocalPosition + new Vector3(0f, yOffset, 0f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && interactionMark != null)
        {
            interactionMark.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && interactionMark != null)
        {
            interactionMark.SetActive(false);
            interactionMark.transform.localPosition = originalLocalPosition;
        }
    }
}

