using UnityEngine;

public class IceCard : ThrowCard{
    private readonly IceCardInfoSO _info;
    public IceCard(IceCardInfoSO cardInfo, int position) : base(cardInfo, position) {
    }

    protected override void OnThrowHit(RaycastHit? hit) {
        if (!hit.HasValue) {
            RegisterUse();
            return;
        }

        var rhit = hit.Value;
        
        if (rhit.collider.gameObject.TryGetComponent(out ObjectStatus status)) {
            status.ApplyEffect(new FrozenEffect(5f));
        }
        RegisterUse();
    }
}