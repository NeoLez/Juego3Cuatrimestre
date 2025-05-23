﻿using System;
using NonMonobehaviorUpdates;

public abstract class Card : ITickableUpdate, ITickableFixedUpdate {
    public bool Enabled { get; private set; }
    protected int Position;
    protected byte Uses;
    public event Action OnEnabled;
    public event Action OnDisabled;

    public Card(CardInfoSO cardInfo, int position) {
        Position = position;
        Uses = cardInfo.maxUses;

        UpdatesManager.RegisterFixedUpdate(this);
        UpdatesManager.RegisterUpdate(this);

        GameManager.Input.CardUsage.CardUseSelf.started += _ => {
            if (Enabled)
                OnSelfActivation();
        };
        GameManager.Input.CardUsage.CardUseThrow.started += _ => {
            if (Enabled) {
                OnThrowActivation();
            }
        };
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

    public virtual void OnUpdate() {
    }

    public virtual void OnFixedUpdate() {
    }

    protected virtual void OnSelfActivation() {
    }

    protected virtual void OnThrowActivation() {
    }

    protected void RegisterUse(int useAmount = 1) {
        if (--Uses <= useAmount) {
            GameManager.Player.GetComponent<CardStorage>().RemoveCard(Position);
            OnUsesExhausted();
        }
    }

    protected virtual void OnUsesExhausted() {
        Enabled = false;
        UpdatesManager.DeregisterFixedUpdate(this);
        UpdatesManager.DeregisterUpdate(this);
    }
}