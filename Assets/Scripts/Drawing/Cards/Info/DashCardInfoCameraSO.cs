using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "SO/Spells/DashSpellCamera")]
public class DashCardInfoCameraSO : CardInfoSO {
    [SerializeField] public float moveDistance;
    [SerializeField] public float time;
    [SerializeField] public AnimationCurve curve; 
    
    public override Card GetCard(int position) {
        return new DashCard(this, position);
    }
}