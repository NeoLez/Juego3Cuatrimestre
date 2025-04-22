using UnityEngine;

public class Draw : MonoBehaviour
{
    private PlayerInputActions _input;
    private DrawingSurface _currentSurface;
    [SerializeField] private LayerMask Layer;

    private void Awake() {
        _input = GameManager.Input;
    }

    void Update() {
        if (!_input.BookActions.DrawButton.IsPressed()) {
            if (_currentSurface is not null) {
                _currentSurface.FinishDrawing();
                
                _currentSurface = null;
            }

            return;
        }
        
        
        Ray ray = Camera.main.ScreenPointToRay(_input.BookActions.MousePosition.ReadValue<Vector2>());
        if (!Physics.Raycast(ray, out RaycastHit hit, 50f, Layer)) {
            return;
        }

        if (_currentSurface is null) {
            if (hit.collider.TryGetComponent<DrawingSurface>(out var drawingSurface)) {
                _currentSurface = drawingSurface;
            }
            else {
                return;
            }
        }
        

        if (_currentSurface.gameObject == hit.collider.gameObject) {
            _currentSurface.NotifyPosition(hit.textureCoord);            
        }
        
    }
}
