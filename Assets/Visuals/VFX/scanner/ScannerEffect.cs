using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerEffect : MonoBehaviour
{
    public float maxScanRadius = 10f;
    public float scanSpeed = 5f; 
    public float scanDuration = 2f;
    public LayerMask scanMask;
    public string targetTag = "Scanneable";

    private float currentRadius = 0f;
    private float timer = 0f;
    private bool isScanning = true;
    private HashSet<Collider> alreadyDetected = new HashSet<Collider>();

    void Update()
    {
        if (!isScanning) return;

        timer += Time.deltaTime;
        currentRadius = Mathf.Lerp(0f, maxScanRadius, timer / scanDuration);

        Collider[] hits = Physics.OverlapSphere(transform.position, currentRadius);
        foreach (Collider hit in hits)
        {
            if (!alreadyDetected.Contains(hit) && hit.CompareTag(targetTag))
            {
                var scannable = hit.GetComponent<ScannableObject>();
                if (scannable != null)
                {
                    scannable.OnScanned();
                }
                alreadyDetected.Add(hit);
            }
        }

        if (timer >= scanDuration)
        {
            isScanning = false;
            Destroy(gameObject); // opcional: destruye el scanner cuando termina
        }
    }

    void OnDrawGizmos()
    {
        if (isScanning)
        {
            Gizmos.color = new Color(0f, 1f, 1f, 0.3f);
            Gizmos.DrawWireSphere(transform.position, currentRadius);
        }
    }
}
