using Optional;
using Optional.Unsafe;
using UnityEngine;

public class Draw : MonoBehaviour
{
    private PlayerInputActions _input;
    private DrawingSurface _currentSurface;
    private CardStorage _cardStorage;

    private void Awake() {
        _input = new();
        _input.Enable();
        _input.Movement.Enable();

        _cardStorage = GetComponent<CardStorage>();
    }

    void Update() {
        if (!_input.Movement.MouseLeftClick.IsPressed()) {
            if (_currentSurface is not null) {
                _currentSurface.FinishDrawing();
                
                _currentSurface = null;
            }

            return;
        }
        
        
        Ray ray = Camera.main.ScreenPointToRay(_input.Movement.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit)) {
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
