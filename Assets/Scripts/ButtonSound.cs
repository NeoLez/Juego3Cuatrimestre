using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private static AudioSource globalAudioSource;

    public AudioClip hoverSound;
    public AudioClip clickSound;
    
    public GameObject panelOpcionConfirmar;  
    public GameObject panelSalirConfirmar;    

    public bool abreOpciones = false;
    public bool abreSalir = false;
    public bool cierraOpciones = false;
    public bool cierraSalir = false;

    private TextMeshProUGUI buttonText;
    public Color hoverColor = Color.yellow;
    private Color originalColor;

    private void Start()
    {
        if (globalAudioSource == null)
        {
            GameObject audioManager = GameObject.Find("AudioManager");
            if (audioManager == null)
            {
                audioManager = new GameObject("AudioManager");
                globalAudioSource = audioManager.AddComponent<AudioSource>();
                DontDestroyOnLoad(audioManager);
            }
            else
            {
                globalAudioSource = audioManager.GetComponent<AudioSource>();
            }
        }

        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
            originalColor = buttonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
            globalAudioSource.PlayOneShot(hoverSound);

        if (buttonText != null)
            buttonText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
            buttonText.color = originalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
            globalAudioSource.PlayOneShot(clickSound);

        if (buttonText != null)
            buttonText.color = originalColor;

        if (abreOpciones && panelOpcionConfirmar != null)
        {
            StartCoroutine(DelaySetActive(panelOpcionConfirmar, true));
        }
        else if (abreSalir && panelSalirConfirmar != null)
        {
            StartCoroutine(DelaySetActive(panelSalirConfirmar, true));
        }
        else if (cierraOpciones && panelOpcionConfirmar != null)
        {
            StartCoroutine(DelaySetActive(panelOpcionConfirmar, false));
        }
        else if (cierraSalir && panelSalirConfirmar != null)
        {
            StartCoroutine(DelaySetActive(panelSalirConfirmar, false));
        }
    }

    private IEnumerator DelaySetActive(GameObject panel, bool state)
    {
        yield return new WaitForSeconds(clickSound.length);
        panel.SetActive(state);
    }
}
