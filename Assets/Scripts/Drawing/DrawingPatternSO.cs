using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "DrawingPattern", menuName = "SO/Drawing/Pattern")]
public class DrawingPatternSO : ScriptableObject {
    [SerializeField] public Drawing drawing;
    [FormerlySerializedAs("spell")] [SerializeField] public CardInfoSO cardInfo;
}
