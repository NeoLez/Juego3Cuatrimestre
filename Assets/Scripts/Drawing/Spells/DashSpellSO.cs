using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "SO/Spells/DashSpell")]
public class DashSpellSO : SpellSO {
    [SerializeField] private float moveDistance;
    [SerializeField] private float time;
    [SerializeField] private AnimationCurve curve; 
    
    public override void RunSpell() {
        base.RunSpell();
        float yAngle = Camera.main.transform.eulerAngles.y;

        Vector3 forwardVector = new Vector3(Mathf.Cos(yAngle * Mathf.Deg2Rad), 0, -Mathf.Sin(yAngle * Mathf.Deg2Rad));
        Vector3 rightVector = new Vector3(-forwardVector.z, 0, forwardVector.x);
        
        GameManager.Player.GetComponent<MovementControllerTest>().Dash(rightVector, moveDistance, time ,curve);
    }
}