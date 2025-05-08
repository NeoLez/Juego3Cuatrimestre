using UnityEngine;

public class PuzzleZone : MonoBehaviour
{
    public Transform snapPoint;
    public int puzzleIndex; 
    public AudioClip placeBoxSound;

    private bool isOccupied = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOccupied && other.GetComponent<Box>() != null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;

            other.transform.position = snapPoint.position;
            other.transform.rotation = snapPoint.rotation;

            isOccupied = true;

            if (placeBoxSound != null)
                audioSource.PlayOneShot(placeBoxSound);

            PuzzleManager.Instance.PlaceBoxInPuzzle(puzzleIndex);
        }
    }
}