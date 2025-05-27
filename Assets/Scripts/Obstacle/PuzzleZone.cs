using UnityEngine;
using UnityEngine.UIElements;

public class PuzzleZone : MonoBehaviour
{
    public Transform snapPoint;
    public int puzzleIndex;
    public AudioClip placeBoxSound;

    public PuzzleManager puzzleManager; // 👈 Nueva referencia

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
            rb.isKinematic = true;
            GameManager.Player.GetComponent<Drag>().DisengageObject(other.gameObject);
            other.transform.position = snapPoint.position;
            other.transform.rotation = snapPoint.rotation;

            isOccupied = true;

            if (placeBoxSound != null)
                audioSource.PlayOneShot(placeBoxSound);

            // 🔁 Usamos la referencia pública
            if (puzzleManager != null)
                puzzleManager.PlaceBoxInPuzzle(puzzleIndex);
        }
    }
}