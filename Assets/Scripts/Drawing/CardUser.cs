using UnityEngine;

public class CardUser : MonoBehaviour {
    private PlayerInputActions _input;
    private CardStorage _cardStorage;
    
    private void Awake() {
        _input = new PlayerInputActions();
        _input.Enable();
        _input.Movement.Enable();

        _cardStorage = GetComponent<CardStorage>();

        _input.Movement.UseCard1.performed += (context => UseCard(1));
        _input.Movement.UseCard2.performed += (context => UseCard(2));
        _input.Movement.UseCard3.performed += (context => UseCard(3));
        _input.Movement.UseCard4.performed += (context => UseCard(4));
    }

    private void UseCard(byte i) {
        _cardStorage.UseCard(i);
    }
}