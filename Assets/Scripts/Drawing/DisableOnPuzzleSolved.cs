using UnityEngine;

[RequireComponent(typeof(DrawingSurfacePuzzle))]
public class DisableOnPuzzleSolved : MonoBehaviour {
    [SerializeField] private GameObject gameObjectToDisable;
    private void Awake() {
        GetComponent<DrawingSurfacePuzzle>().OnPuzzleSolved += () => {
            gameObjectToDisable.SetActive(false);
        };
    }
}