using UnityEngine;

public class CardStorage : MonoBehaviour {
    private Card[] _cards = new Card[CARDS_MAX];
    [SerializeField] private GameObject[] _CardUIGameObjects = new GameObject[6];
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private GameObject CardPrefab;
    private const int CARDS_MAX = 6;

    public bool AddCard(CardInfoSO cardInfo) {
        for (byte i = 0; i < CARDS_MAX; i++) {
            if (_cards[i] == null) {
                _cards[i] = cardInfo.GetCard(i);

                GameObject card = Instantiate(CardPrefab, UIPanel.transform);
                card.transform.SetSiblingIndex(i);
                _CardUIGameObjects[i] = card;
                card.GetComponent<RectTransform>().anchoredPosition = Vector2.right * 120 * i;
                
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
        Destroy(_CardUIGameObjects[i]);
        _CardUIGameObjects[i] = null;
    }
}