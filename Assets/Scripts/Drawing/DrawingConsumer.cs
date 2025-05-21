using UnityEngine;

public abstract class DrawingConsumer : MonoBehaviour {
    public abstract bool Consume(Drawing drawing, int amountOfPoints);
}