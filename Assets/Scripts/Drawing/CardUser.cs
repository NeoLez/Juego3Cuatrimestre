using UnityEngine;

public class CardUser : MonoBehaviour {
    private PlayerInputActions _input;
    private CardStorage _cardStorage;
    
    private void Awake() {
        _input = new PlayerInputActions();
        _input.Enable();
        _input.Movement.Enable();

        _cardStorage = GetComponent<CardStorage>();

        _input.Movement.UseCard0.performed += (context => SetCurrentCard(0));
        _input.Movement.UseCard1.performed += (context => SetCurrentCard(1));
        _input.Movement.UseCard2.performed += (context => SetCurrentCard(2));
        _input.Movement.UseCard3.performed += (context => SetCurrentCard(3));
        _input.Movement.UseCard4.performed += (context => SetCurrentCard(4));
        _input.Movement.UseCard5.performed += (context => SetCurrentCard(5));
    }

    private void SetCurrentCard(byte i) {
        _cardStorage.SetCurrentCard(i);
    }
}