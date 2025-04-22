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
            Quaternion deltaRotation = currentPlatform.rotation * Quaternion.Inverse(lastPlatformRotation);

            Vector3 offset = transform.position - currentPlatform.position;
            offset = deltaRotation * offset;
            transform.position = currentPlatform.position + offset;

            transform.rotation = deltaRotation * transform.rotation;

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