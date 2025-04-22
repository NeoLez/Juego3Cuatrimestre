using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInThePlataform : MonoBehaviour
{
    private Transform currentPlatform;
    private Quaternion lastPlatformRotation;

    void FixedUpdate()
    {
        if (currentPlatform != null)
        {
            // Calcula cu�nto rot� la plataforma desde el �ltimo frame
            Quaternion deltaRotation = currentPlatform.rotation * Quaternion.Inverse(lastPlatformRotation);

            // Aplica esa rotaci�n al jugador alrededor del centro de la plataforma
            Vector3 offset = transform.position - currentPlatform.position;
            offset = deltaRotation * offset;
            transform.position = currentPlatform.position + offset;

            // (Opcional) rota tambi�n el jugador si quer�s que se vea afectado visualmente
            transform.rotation = deltaRotation * transform.rotation;

            // Guarda rotaci�n para el siguiente frame
            lastPlatformRotation = currentPlatform.rotation;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RotatingPlatform"))
        {
            currentPlatform = collision.transform;
            lastPlatformRotation = currentPlatform.rotation;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("RotatingPlatform"))
        {
            if (collision.transform == currentPlatform)
            {
                currentPlatform = null;
            }
        }
    }
}