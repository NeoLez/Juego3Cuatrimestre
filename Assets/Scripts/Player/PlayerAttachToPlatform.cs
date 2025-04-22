using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttachToPlatform : MonoBehaviour
{
    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;
    private Quaternion lastPlatformRotation;

    void FixedUpdate()
    {
        if (currentPlatform != null)
        {
            Vector3 platformMovement = currentPlatform.position - lastPlatformPosition;
            Quaternion platformRotationDelta = currentPlatform.rotation * Quaternion.Inverse(lastPlatformRotation);

            transform.position += platformMovement;
            transform.rotation = platformRotationDelta * transform.rotation;

            lastPlatformPosition = currentPlatform.position;
            lastPlatformRotation = currentPlatform.rotation;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RotatingPlatform"))
        {
            currentPlatform = collision.transform;
            lastPlatformPosition = currentPlatform.position;
            lastPlatformRotation = currentPlatform.rotation;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("RotatingPlatform"))
        {
            currentPlatform = null;
        }
    }
}
