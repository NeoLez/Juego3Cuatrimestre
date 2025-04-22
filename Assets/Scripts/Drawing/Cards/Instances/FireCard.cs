using UnityEngine;

public class FireCard : ThrowCard {
    private FireCardInfoSO _info;
    public FireCard(FireCardInfoSO cardInfo, int position) : base(cardInfo, position) {
    }

    protected override void OnThrowHit(RaycastHit? hit) {
        if (!hit.HasValue) {
            RegisterUse();
            return;
        }

        var rhit = hit.Value;
        
        if (rhit.collider.gameObject.TryGetComponent(out ObjectStatus status)) {
            status.ApplyEffect(new BurningEffect(4f));
        }
        RegisterUse();
    }
}