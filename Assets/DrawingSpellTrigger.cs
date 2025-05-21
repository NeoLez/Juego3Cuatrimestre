using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingSpellTrigger : MonoBehaviour
{
    [SerializeField] private DrawingSurfacePuzzle puzzle;
    [SerializeField] private ObjectStatus targetObject;
    [SerializeField] private SpellType spellToCast;

    [Header("Opcional para Dash")]
    [SerializeField] private Transform blockToMove;
    [SerializeField] private Vector3 offset = new Vector3(3f, 0, 0);
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private float waitTime = 3f;

    private Vector3 originalPosition;
    private bool isMoving = false;

    private void Awake()
    {
        if (blockToMove != null)
            originalPosition = blockToMove.position;
    }

    private void OnEnable()
    {
        if (puzzle != null)
            puzzle.OnPuzzleSolved += OnSpellTrigger;
    }

    private void OnDisable()
    {
        if (puzzle != null)
            puzzle.OnPuzzleSolved -= OnSpellTrigger;
    }

    private void OnSpellTrigger()
    {
        switch (spellToCast)
        {
            case SpellType.Fire:
                targetObject.ApplyEffect(new BurningEffect(5f));
                break;
            case SpellType.Ice:
                targetObject.ApplyEffect(new FrozenEffect(5f));
                break;
            case SpellType.Dash:
                TryMoveBlock();
                break;
        }
    }

    private void TryMoveBlock()
    {
        if (blockToMove == null || isMoving) return;

        isMoving = true;
        Vector3 targetPos = originalPosition + offset;

        LeanTween.move(blockToMove.gameObject, targetPos, moveDuration).setOnComplete(() =>
        {
            Invoke(nameof(MoveBack), waitTime);
        });
    }

    private void MoveBack()
    {
        LeanTween.move(blockToMove.gameObject, originalPosition, moveDuration).setOnComplete(() =>
        {
            isMoving = false;
        });
    }
}

public enum SpellType
{
    Fire,
    Ice,
    Dash
}
