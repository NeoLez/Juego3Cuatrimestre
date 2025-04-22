using UnityEngine;

public class DashCard : ThrowCard {
    private readonly DashCardInfoCameraSO _info;
    private float dashFOV = 80f;
    private float dashInTime = 0.05f;
    private float dashOutTime = 0.2f;
    public DashCard(DashCardInfoCameraSO cardInfo, int position) : base(cardInfo, position) {
        _info = cardInfo;
    }

    protected override void OnSelfActivation() {
        base.OnSelfActivation();
        GameManager.Player.GetComponent<MovementControllerTest>().Dash(GameManager.MainCamera.transform.forward, _info.moveDistance, _info.time ,_info.curve);
        LeanTween.cancel(GameManager.MainCamera.gameObject);
        LeanTween.value(GameManager.MainCamera.gameObject, GameManager.CamFOV, dashFOV, dashInTime)
            .setOnUpdate((float fov) => GameManager.MainCamera.fieldOfView = fov)
            .setOnComplete(() => {
                LeanTween.value(GameManager.MainCamera.gameObject, dashFOV, GameManager.CamFOV, dashOutTime)
                    .setOnUpdate((float fov) => GameManager.MainCamera.fieldOfView = fov);
            });
        RegisterUse();
    }

    protected override void OnThrowHit(RaycastHit? hit) {
        if (!hit.HasValue) {
            RegisterUse();
            return;
        }
        
        var rhit = hit.Value;
        if(rhit.rigidbody != null)
            rhit.rigidbody.AddForce(_info.forceStrength * (rhit.point - GameManager.MainCamera.transform.position), ForceMode.VelocityChange);
        RegisterUse();
    }
}