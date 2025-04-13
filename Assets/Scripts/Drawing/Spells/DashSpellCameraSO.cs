using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "SO/Spells/DashSpellCamera")]
public class DashSpellCameraSO : SpellSO {
    [SerializeField] private float moveDistance;
    [SerializeField] private float time;
    [SerializeField] private AnimationCurve curve; 
    
    public override void RunSpell() {
        base.RunSpell();
        
        GameManager.Player.GetComponent<MovementControllerTest>().Dash(Camera.main.transform.forward, moveDistance, time ,curve);
    }
}