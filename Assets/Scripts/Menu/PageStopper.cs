using UnityEngine;

public class PageStopper : MonoBehaviour
{
    public Animator animator;
    public Transform paginaTransform;
    public float anguloObjetivo = 180f;
    public float tolerancia = 10f; // Podés ajustarlo
    public float tiempoEspera = 1.0f;
    public float desaceleracion = 1f; // Qué tan rápido se desacelera

    private bool puedeVerificar = false;
    private bool desacelerando = false;
    private bool detenido = false;

    void Start()
    {
        animator.speed = 1f;
        Invoke(nameof(HabilitarChequeo), tiempoEspera);
    }

    void HabilitarChequeo()
    {
        puedeVerificar = true;
    }

    void Update()
    {
        if (!puedeVerificar || detenido) return;

        float anguloY = NormalizarAngulo(paginaTransform.localEulerAngles.y);
        float objetivoNormalizado = NormalizarAngulo(anguloObjetivo);

        // Si está dentro del rango de tolerancia, empieza a desacelerar
        if (Mathf.Abs(anguloY - objetivoNormalizado) <= tolerancia)
        {
            desacelerando = true;
        }

        if (desacelerando)
        {
            animator.speed = Mathf.Max(0f, animator.speed - desaceleracion * Time.deltaTime);

            if (animator.speed <= 0.01f)
            {
                animator.speed = 0f;
                detenido = true;
                desacelerando = false;
            }
        }
    }

    float NormalizarAngulo(float angulo)
    {
        angulo %= 360;
        if (angulo > 180) angulo -= 360;
        if (angulo < -180) angulo += 360;
        return angulo;
    }
}