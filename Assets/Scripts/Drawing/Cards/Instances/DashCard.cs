public class DashCard : Card {
    private readonly DashCardInfoCameraSO _info;
    public DashCard(DashCardInfoCameraSO cardInfo, byte position) : base(cardInfo, position) {
        _info = cardInfo;
    }

    protected override void OnSelfActivation() {
        base.OnSelfActivation();
        GameManager.Player.GetComponent<MovementControllerTest>().Dash(GameManager.MainCamera.transform.forward, _info.moveDistance, _info.time ,_info.curve);
        RegisterUse();
    }
}