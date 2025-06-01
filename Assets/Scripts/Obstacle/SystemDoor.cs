using System.Collections;
using UnityEngine;

public class SystemDoor : MonoBehaviour
{
    public enum DoorType
    {
        Rotate,
        MoveUp,
        MoveDown,
        SlideLeft,
        SlideRight
    }

    public DoorType doorType = DoorType.Rotate;

    [Header("Rotación")]
    public float doorOpenAngle = 95f;

    [Header("Movimiento Lineal")]
    public float moveDistance = 3f;

    [Header("General")]
    public float smooth = 3.0f;

    private bool doorOpened = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    public void OpenDoor()
    {
        if (!doorOpened)
        {
            StartCoroutine(OpenDoorCoroutine());
            doorOpened = true;
        }
    }

    IEnumerator OpenDoorCoroutine()
    {
        switch (doorType)
        {
            case DoorType.Rotate:
                Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
                while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.1f)
                {
                    transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
                    yield return null;
                }
                break;

            case DoorType.MoveUp:
                yield return MoveDoor(initialPosition + Vector3.up * moveDistance);
                break;

            case DoorType.MoveDown:
                yield return MoveDoor(initialPosition + Vector3.down * moveDistance);
                break;

            case DoorType.SlideLeft:
                yield return MoveDoor(initialPosition + Vector3.left * moveDistance);
                break;

            case DoorType.SlideRight:
                yield return MoveDoor(initialPosition + Vector3.right * moveDistance);
                break;
        }
    }

    IEnumerator MoveDoor(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, smooth * Time.deltaTime);
            yield return null;
        }
    }
}
