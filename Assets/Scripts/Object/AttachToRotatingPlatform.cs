using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AttachToRotatingPlatform : MonoBehaviour
{
    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;
    private Quaternion lastPlatformRotation;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (currentPlatform != null)
        {
            Vector3 platformMovement = currentPlatform.position - lastPlatformPosition;
            Quaternion platformRotationDelta = currentPlatform.rotation * Quaternion.Inverse(lastPlatformRotation);

            rb.MovePosition(rb.position + platformMovement);
            rb.MoveRotation(platformRotationDelta * rb.rotation);

            lastPlatformPosition = currentPlatform.position;
            lastPlatformRotation = currentPlatform.rotation;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RotatingPlatform"))
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                // Solo se acopla si el cubo está por encima de la plataforma (check en eje Y)
                if (contact.point.y > collision.transform.position.y + 0.1f)
                {
                    currentPlatform = collision.transform;
                    lastPlatformPosition = currentPlatform.position;
                    lastPlatformRotation = currentPlatform.rotation;
                    break;
                }
            }
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
