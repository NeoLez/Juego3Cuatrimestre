using UnityEngine;
using UnityEngine.Serialization;

public class CardStorage : MonoBehaviour {
    private readonly Card[] _cards = new Card[CardsMax];
    [FormerlySerializedAs("_CardUIGameObjects")] [SerializeField] private GameObject[] cardUIGameObjects = new GameObject[6];
    [FormerlySerializedAs("UIPanel")] [SerializeField] private GameObject uiPanel;
    private const int CardsMax = 6;

    public bool AddCard(CardInfoSO cardInfo) {
        for (byte i = 0; i < CardsMax; i++) {
            if (_cards[i] == null) {
                _cards[i] = cardInfo.GetCard(i);

                GameObject card = Instantiate(cardInfo.cardUIPrefab, uiPanel.transform);
                card.transform.SetSiblingIndex(i);
                cardUIGameObjects[i] = card;
                card.GetComponent<RectTransform>().anchoredPosition = Vector2.right * (120 * i);
                
                return true;
            }
        }

        return false;
    }

    public void SetCurrentCard(byte i) {
        foreach (var card in _cards) {
            card?.Disable();
        }
        _cards[i]?.Enable();
    }

    public void RemoveCard(byte i) {
        _cards[i] = null;
        Destroy(cardUIGameObjects[i]);
        cardUIGameObjects[i] = null;
    }
}