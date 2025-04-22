using UnityEngine;
using UnityEngine.Serialization;

public class EffectTester : MonoBehaviour
{
    public float range = 2f;
    public LayerMask targetLayer;
    public Camera cam;

    private void Start()
    {
        cam = GameManager.MainCamera;
        GameManager.Input.Movement.TestFire.started += _ => ApplyEffect(new BurningEffect(5f));
        GameManager.Input.Movement.TestIce.started += _ => ApplyEffect(new FrozenEffect(3f));
    }

    void ApplyEffect(StatusEffect effect)
    {
        // Detecta un objeto frente al jugador dentro de cierto rango
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        
        //TODO: Can't use layer here. Maybe calculating all hits and then finding the first one that has the ObjectType
        //component and a value that can be processed counts as a hit, and the effect is applied only to that object
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log($"Raycast hit: {hit.collider.name}");

            var status = hit.collider.GetComponent<ObjectStatus>();
            if (status != null)
            {
                Debug.Log("t");
                status.ApplyEffect(effect);
            }
        }
    }

    // Gizmo para visualizar el rayo en la escena
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * range);
    }
}
