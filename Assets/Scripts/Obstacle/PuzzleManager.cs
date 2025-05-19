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
    [Header("Puzzle List")]
    public List<PuzzleData> puzzles = new List<PuzzleData>();

    public void PlaceBoxInPuzzle(int puzzleIndex)
    {
        if (puzzleIndex < 0 || puzzleIndex >= puzzles.Count) return;

        PuzzleData puzzle = puzzles[puzzleIndex];

        if (puzzle.completed) return;

        puzzle.placedBoxes++;

        if (puzzle.placedBoxes >= puzzle.requiredBoxes)
        {
            puzzle.completed = true;

            if (puzzle.door != null)
            {
                SystemDoor doorScript = puzzle.door.GetComponent<SystemDoor>();
                if (doorScript != null)
                {
                    doorScript.OpenDoor();
                }
            }

            if (puzzle.doorOpenSound != null)
                AudioSource.PlayClipAtPoint(puzzle.doorOpenSound, puzzle.door.transform.position);
        }
    }
}