using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public Vector3 puntoA;
    public Vector3 puntoB;
    private Vector3 startPosition;
    public float speed = 2f;

    private float originalSpeed;
    private float t = 0f;
    private bool goingTowardB = true;

    void Start() {
        startPosition = transform.position;
        originalSpeed = speed;
    }

    void Update()
    {
        t += Time.deltaTime * speed * (goingTowardB ? 1 : -1);
        transform.position = Vector3.Lerp(startPosition + puntoA, startPosition + puntoB, t);

        if (t >= 1f)
        {
            t = 1f;
            goingTowardB = false;
        }
        else if (t <= 0f)
        {
            t = 0f;
            goingTowardB = true;
        }
    }

    public void Freeze(float factor, float duracion)
    {
        speed *= factor;
        Invoke(nameof(RestoreSpeed), duracion);
    }

    void RestoreSpeed()
    {
        speed = originalSpeed;
    }
}
