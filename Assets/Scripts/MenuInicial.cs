using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    [Header("Nombre de la escena ")]
    public string nombreEscenaAJugar = "NombreDeTuEscena"; 

    public void Jugar()
    {
        TransicionEscenasUI.instance.DisolverSalida(nombreEscenaAJugar);
    }

    public void Salir()
    {
        Debug.Log("Saliste xd");
        Application.Quit();
    }
}