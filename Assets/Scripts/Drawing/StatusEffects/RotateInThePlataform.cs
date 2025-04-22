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
            // Calcula cuánto rotó la plataforma desde el último frame
            Quaternion deltaRotation = currentPlatform.rotation * Quaternion.Inverse(lastPlatformRotation);

            // Aplica esa rotación al jugador alrededor del centro de la plataforma
            Vector3 offset = transform.position - currentPlatform.position;
            offset = deltaRotation * offset;
            transform.position = currentPlatform.position + offset;

            // (Opcional) rota también el jugador si querés que se vea afectado visualmente
            transform.rotation = deltaRotation * transform.rotation;

            // Guarda rotación para el siguiente frame
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