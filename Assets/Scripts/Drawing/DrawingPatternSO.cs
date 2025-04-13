using UnityEngine;

[CreateAssetMenu(fileName = "DrawingPattern", menuName = "SO/Drawing/Pattern")]
public class DrawingPatternSO : ScriptableObject {
    [SerializeField] public Drawing drawing;
    [SerializeField] public SpellSO spell;
}
