public class DashCard : Card {
    private readonly DashCardInfoCameraSO _info;
    public DashCard(DashCardInfoCameraSO cardInfo, byte position) : base(cardInfo, position) {
        _info = cardInfo;
    }
    
    protected override void OnEnable() {
        base.OnEnable();
        GameManager.Player.GetComponent<MovementControllerTest>().Dash(GameManager.MainCamera.transform.forward, _info.moveDistance, _info.time ,_info.curve);
        if (--Uses <= 0) {
            GameManager.Player.GetComponent<CardStorage>().RemoveCard(Position);
        }
    }
}