using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int daño = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth salud = collision.gameObject.GetComponent<PlayerHealth>();
            if (salud != null)
            {
                salud.RecibirDaño(daño);
                Debug.Log("jaja moriste");
            }
        }
    }
}
