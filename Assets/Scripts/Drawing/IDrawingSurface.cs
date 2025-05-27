using UnityEngine;

public interface IDrawingSurface {
    public void NotifyPosition(Vector2 position);
    public void FinishDrawing();
}