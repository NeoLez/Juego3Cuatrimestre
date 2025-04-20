using System;

public abstract class Card {
    protected bool Enabled;
    protected byte Position;
    protected byte Uses;
    public event Action OnEnabled;
    public event Action OnDisabled;

    public Card(CardInfoSO cardInfo, byte position) {
        Position = position;
        Uses = cardInfo.maxUses;
    }

    public void Enable() {
        if (!Enabled) {
            Enabled = true;
            OnEnabled?.Invoke();
            OnEnable();
        }
    }
    public void Disable() {
        if (Enabled) {
            Enabled = false;
            OnDisabled?.Invoke();
            OnDisable();
        }
    }

    protected virtual void OnEnable() {
        
    }

    protected virtual void OnDisable() {
        
    }
}