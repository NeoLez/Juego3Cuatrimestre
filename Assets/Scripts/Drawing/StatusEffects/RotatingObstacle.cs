using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    [Tooltip("Velocidad de rotación en grados por segundo")]
    public float Speed = 180f;

    [Tooltip("Eje de rotación.")]
    public Vector3 rotationAxis = Vector3.up;

    private float originalSpeed;
    private Quaternion lastRotation;

    private List<Rigidbody> objetosEncima = new List<Rigidbody>();

    void Start()
    {
        originalSpeed = Speed;
        lastRotation = transform.rotation;
    }

    void Update()
    {
        transform.Rotate(rotationAxis.normalized, Speed * Time.deltaTime, Space.Self);

        Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(lastRotation);

        foreach (Rigidbody rb in objetosEncima)
        {
            if (rb != null)
            {
                Vector3 dir = rb.position - transform.position;
                dir = deltaRotation * dir;
                Vector3 newPos = transform.position + dir;

                rb.MovePosition(newPos);
            }
        }

        lastRotation = transform.rotation;
    }

    public void Freeze(float factor, float duracion)
    {
        Speed *= factor;
        Invoke(nameof(RestoreSpeed), duracion);
    }

    void RestoreSpeed()
    {
        Speed = originalSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null && !objetosEncima.Contains(rb))
        {
            objetosEncima.Add(rb);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null && objetosEncima.Contains(rb))
        {
            objetosEncima.Remove(rb);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + rotationAxis.normalized);
        Gizmos.DrawSphere(transform.position + rotationAxis.normalized, 0.1f);
    }
}
