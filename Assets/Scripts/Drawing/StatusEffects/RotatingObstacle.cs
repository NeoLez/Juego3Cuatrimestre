using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    [Tooltip("Velocidad de rotación en grados por segundo")]
    public float Speed = 180f;

    [Tooltip("Eje de rotación. Ej: (1, 0, 0) para eje X, (0, 1, 0) para eje Y, etc.")]
    public Vector3 rotationAxis = Vector3.up;

    private float originalSpeed;

    void Start()
    {
        originalSpeed = Speed;
    }

    void Update()
    {
        transform.Rotate(rotationAxis.normalized, Speed * Time.deltaTime);
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + rotationAxis.normalized);
        Gizmos.DrawSphere(transform.position + rotationAxis.normalized, 0.1f);
    }
}
