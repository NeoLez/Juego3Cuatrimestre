using System.Collections.Generic;
using System.Linq;
using Optional;
using Optional.Unsafe;
using UnityEngine;

public class DrawingSurface : MonoBehaviour {
    [SerializeField] private DrawingPoint[] points;
    [SerializeField] private Material lineMaterial;
    [SerializeField] private float lineOffset;
    private readonly List<LineRenderer> _lineRenderers = new();
    private LineRenderer _connectionsRenderer;
    private List<OrderAgnosticByteTuple> _tuples = new(4);
    private byte? _lastPoint;
    
    public void FinishDrawing() {
        foreach (var point in points) {
            point.selected = false;
        }

        Drawing res = new Drawing(_tuples.ToHashSet().ToArray());
        Option<SpellSO> s = DrawingPatternDatabase.GetSpellFromDrawing(res);
        if (s.HasValue) {
            //WHAT IT DOES WITH THE VALUE COULD BE CHANGED TO HAVE DIFFERENT KINDS OF DRAWING SURFACES, MAYBE HAVING
            //A CALLBACK EXTERNAL SCRIPTS CAN SUBSCRIBE TO WOULD BE GOOD
            CardStorage cardStorage = GameManager.Player.GetComponent<CardStorage>();
            cardStorage.AddCard(s.ValueOrFailure());
        }

        _tuples.Clear();
        _lastPoint = null;
    }

    public void NotifyPosition(Vector2 position) {
        Vector2 unscaled = PosToUnscaled(position);

        for (byte i = 0; i< points.Length; i++) {
            Vector2 pointUnscaled = PosToUnscaled(points[i].position);
            if (Vector2.Distance(pointUnscaled, unscaled) <= points[i].size) {
                points[i].selected = true;
                if (_lastPoint is not null) {
                    if (_lastPoint != i) {
                        OrderAgnosticByteTuple newTuple = new OrderAgnosticByteTuple(_lastPoint.Value, i);
                        //if (!_tuples.Contains(newTuple)) { THIS CAUSES VISUAL BUGS BECAUSE THE LINE RENDERER SUCKS
                        //THIS FIX ALSO CAUSES LAG IF THE PLAYER DRAWS TOO MANY LINES :C
                        _tuples.Add(newTuple);
                    }
                }
                _lastPoint = i;
            }
        }
    }

    private void LateUpdate() {
        DrawGraphics();
    }

    public void DrawGraphics() {
        Vector3 offsetVector = transform.rotation * new Vector3(0, 0, -lineOffset);
        
        if (_connectionsRenderer == null) {
            GameObject aux = new GameObject();
            _connectionsRenderer = aux.AddComponent<LineRenderer>();
            _connectionsRenderer.material = lineMaterial;
            _connectionsRenderer.endColor = _connectionsRenderer.startColor = Color.black;
            _connectionsRenderer.widthCurve = new AnimationCurve(new Keyframe(0f, 0.1f));
        }

        if (_tuples.Count > 0) {
            _connectionsRenderer.positionCount = _tuples.Count + 1;
            for (int i = 0; i < _tuples.Count; i++) {
                _connectionsRenderer.SetPosition(i, transform.rotation * PosToUnscaled(points[_tuples[i].firstByte].position - Vector2.one / 2) +
                                                    transform.position + offsetVector*2);
            }

            _connectionsRenderer.SetPosition(_tuples.Count,
                transform.rotation *
                PosToUnscaled(points[_tuples[^1].secondByte].position - Vector2.one / 2) +
                transform.position + offsetVector*2);
        }
        else {
            _connectionsRenderer.positionCount = 0;
        }


        
        if (points.Length > _lineRenderers.Count) {
            int diff = points.Length - _lineRenderers.Count;
            for (int i = 0; i < diff; i++) {
                GameObject aux = new GameObject();
                _lineRenderers.Add(aux.AddComponent<LineRenderer>());
                _lineRenderers[i].material = lineMaterial;
                _lineRenderers[i].loop = true;
                _lineRenderers[i].widthCurve = new AnimationCurve(new Keyframe(0f, 0.05f));
                _lineRenderers[i].positionCount = 4;
                
                //TODO changing this prevents clipping, but we need to figure out how to have it render in the right orientation
                //_lineRenderers[i].alignment = LineAlignment.TransformZ;
            }
        }
        
        for (int i = 0; i < points.Length; i++) {
            _lineRenderers[i].endColor = _lineRenderers[i].startColor = points[i].selected ? Color.green : Color.red;
            
            Vector3[] a = {
                transform.rotation * (PosToUnscaled(points[i].position - Vector2.one / 2) + Vector2.up * points[i].size) +
                transform.position + offsetVector,
                transform.rotation * (PosToUnscaled(points[i].position - Vector2.one / 2) + Vector2.right * points[i].size) +
                transform.position + offsetVector,
                transform.rotation * (PosToUnscaled(points[i].position - Vector2.one / 2) + Vector2.down * points[i].size) +
                transform.position + offsetVector,
                transform.rotation * (PosToUnscaled(points[i].position - Vector2.one / 2) + Vector2.left * points[i].size) +
                transform.position + offsetVector,
            };
            _lineRenderers[i].SetPositions(a);
        }
    }

    private Vector2 PosToUnscaled(Vector2 v) {
        return new Vector2(v.x * transform.localScale.x, v.y * transform.localScale.y);
    }
}
