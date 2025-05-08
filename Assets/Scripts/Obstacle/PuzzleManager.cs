using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzleData
{
    public string name;
    public GameObject door;
    public int requiredBoxes = 2;
    [HideInInspector] public int placedBoxes = 0;
    [HideInInspector] public bool completed = false;
    public AudioClip doorOpenSound;
}

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    [Header("Puzzle List")]
    public List<PuzzleData> puzzles = new List<PuzzleData>();

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlaceBoxInPuzzle(int puzzleIndex)
    {
        // Valid index check
        if (puzzleIndex < 0 || puzzleIndex >= puzzles.Count) return;

        PuzzleData puzzle = puzzles[puzzleIndex];

        // Avoid repeating completion
        if (puzzle.completed) return;

        puzzle.placedBoxes++;

        if (puzzle.placedBoxes >= puzzle.requiredBoxes)
        {
            puzzle.completed = true;

            // Deactivate door
            if (puzzle.door != null)
                puzzle.door.SetActive(false);

            // Play sound
            if (puzzle.doorOpenSound != null && puzzle.door != null)
                AudioSource.PlayClipAtPoint(puzzle.doorOpenSound, puzzle.door.transform.position);
        }
    }
}