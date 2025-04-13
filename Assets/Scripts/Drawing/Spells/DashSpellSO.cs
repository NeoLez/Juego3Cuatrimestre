using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "SO/DashSpell")]
public class DashSpellSO : SpellSO {
    [SerializeField] private Vector3 worldForce;
    [SerializeField] private Vector3 cameraForce;
    public override void RunSpell() {
        base.RunSpell();

        float yAngle = Camera.main.transform.eulerAngles.y;

        Vector3 forwardVector = new Vector3(Mathf.Cos(yAngle * Mathf.Deg2Rad), 0, -Mathf.Sin(yAngle * Mathf.Deg2Rad));
        Vector3 rightVector = new Vector3(-forwardVector.z, 0, forwardVector.x);
        
        Vector3 resCameraForce = Vector3.zero + forwardVector * cameraForce.x + rightVector * cameraForce.z;
        GameManager.Player.GetComponent<Dashing>().Dash(worldForce + resCameraForce);
    }
}