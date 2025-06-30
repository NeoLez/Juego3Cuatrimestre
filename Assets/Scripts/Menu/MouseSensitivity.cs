using UnityEngine.UI;
using UnityEngine;

public class MouseSensitivity : MonoBehaviour
{
    public Slider sensibilidadSlider;

    void Start()
    {
        float sensibilidad = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        sensibilidadSlider.value = sensibilidad;
        sensibilidadSlider.onValueChanged.AddListener(CambiarSensibilidad);
    }

    public void CambiarSensibilidad(float valor)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", valor);
        PlayerPrefs.Save();
    }
}
