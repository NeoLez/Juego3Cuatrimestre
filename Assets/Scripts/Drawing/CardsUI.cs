using System.Collections.Generic;
using UnityEngine;

public class CardsUI : MonoBehaviour {
    private CardStorage _cardStorage;

    [SerializeField] private GameObject cardPanel;
    [SerializeField] private GameObject cardPrefab;
    private List<GameObject> cardsUIElement = new ();

    private List<GameObject> uiCards = new ();
    private void Start() {
        _cardStorage = GameManager.Player.GetComponent<CardStorage>();
        _cardStorage.CardAdded += CardAdded;
        _cardStorage.CardRemoved += CardRemoved;
    }

    private void CardAdded(SpellSO spell) {
        GameObject card = Instantiate(cardPrefab, cardPanel.transform);
        cardsUIElement.Add(card);
    }

    private void CardRemoved(byte number) {
        Destroy(cardsUIElement[number]);
        cardsUIElement.RemoveAt(number);
    }
}