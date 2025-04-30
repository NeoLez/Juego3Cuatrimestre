using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleZone : MonoBehaviour
{
    public Transform snapPoint; 
    public bool isOccupied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isOccupied && other.CompareTag("PuzzleBox"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;
            
            // posiciona el objeto al snappoint que seria el punto que vos colocas
            other.transform.position = snapPoint.position;
            other.transform.rotation = snapPoint.rotation;

            isOccupied = true;
            PuzzleManager.Instance.BoxPlaced(); 
        }
    }
}
