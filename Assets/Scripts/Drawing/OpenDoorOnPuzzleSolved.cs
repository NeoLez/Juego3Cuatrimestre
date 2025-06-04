using UnityEngine;

[RequireComponent(typeof(DrawingSurfacePuzzle))]
public class OpenDoorOnPuzzleSolved : MonoBehaviour
{
    [SerializeField] private SystemDoor doorToOpen;

    private void Awake()
    {
        GetComponent<DrawingSurfacePuzzle>().OnPuzzleSolved += () => {
            if (doorToOpen != null)
                doorToOpen.OpenDoor();
        };
    }
}