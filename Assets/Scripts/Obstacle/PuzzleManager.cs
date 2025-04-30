using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    public GameObject puerta;
    private int cajasColocadas = 0;

    void Awake()
    {
        Instance = this;
    }

    public void BoxPlaced()
    {
        cajasColocadas++;
        if (cajasColocadas >= 2)
        {
            AbrirPuerta();
        }
    }

    void AbrirPuerta()
    {
        puerta.SetActive(false);
        Debug.Log("abrio xd");
    }
}
