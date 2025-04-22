using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public Vector3 puntoA;         // Posicion inicial
    public Vector3 puntoB;         // Posicion final
    public float velocidad = 2f;   

    private float t = 0f;
    private bool yendoAHaciaB = true;

    void Update()
    {
        t += Time.deltaTime * velocidad * (yendoAHaciaB ? 1 : -1);

        transform.position = Vector3.Lerp(puntoA, puntoB, t);

        if (t >= 1f)
        {
            t = 1f;
            yendoAHaciaB = false;
        }
        else if (t <= 0f)
        {
            t = 0f;
            yendoAHaciaB = true;
        }
    }
}
