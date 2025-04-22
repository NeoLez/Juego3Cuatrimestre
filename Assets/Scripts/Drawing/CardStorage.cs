using UnityEngine;

public class CardStorage : MonoBehaviour {
    private readonly Card[] _cards = new Card[CardsMax];
    [SerializeField] private GameObject[] cardUIGameObjects = new GameObject[CardsMax];
    [SerializeField] private GameObject uiPanel;
    private const int CardsMax = 6;
    [SerializeField] private int CurrentlySelected = -1;
    

    public bool AddCard(CardInfoSO cardInfo) {
        bool result = false;
        for (int i = 0; i < CardsMax; i++) {
            if (_cards[i] == null) {
                _cards[i] = cardInfo.GetCard(i);

                GameObject card = Instantiate(cardInfo.cardUIPrefab, uiPanel.transform);
                card.transform.SetSiblingIndex(i);
                cardUIGameObjects[i] = card;
                card.GetComponent<RectTransform>().anchoredPosition = Vector2.right * (120 * i);
                
                if (i == CurrentlySelected) {
                    cardUIGameObjects[CurrentlySelected].transform.localPosition -= Vector3.up * 20;
                }
                else if(CurrentlySelected > i) {
                    bool foundCard = false;
                    for (int k = i + 1; k < CardsMax && k <= CurrentlySelected; k++) {
                        if (_cards[k] != null) {
                            foundCard = true;
                            break;
                        }
                    }

                    if (!foundCard) {
                        SetCurrentCard(i);
                    }
                }
                
                result = true;
                break;
            }
        }
        
        

        return result;
    }

    public void SetCurrentCard(int pos) {
        _cards[CurrentlySelected]?.Disable();
        if(cardUIGameObjects[CurrentlySelected] != null)
            cardUIGameObjects[CurrentlySelected].transform.localPosition += Vector3.up * 20; 
        CurrentlySelected = pos;
        _cards[pos]?.Enable();
        if(cardUIGameObjects[CurrentlySelected] != null)
            cardUIGameObjects[CurrentlySelected].transform.localPosition -= Vector3.up * 20; 
    }

    public void SetNextCard() {
        int res = GetNextCardRightLooping(CurrentlySelected);
        if(res != -1)
            SetCurrentCard(res);
    }
    
    public void SetPreviousCard() {
        int res = GetNextCardLeftLooping(CurrentlySelected);
        if(res != -1)
            SetCurrentCard(res);
    }

    private int GetNextCardRightLooping(int start) {
        int stepCount = 0;
        int i = (start + 1) % CardsMax;
        while (_cards[i] == null) {
            Debug.Log(i);
            
            if (stepCount == CardsMax - 1) return -1;

            stepCount++;
            i = (i + 1) % CardsMax;
        }

        return i;
    }

    private int GetNextCardLeftLooping(int start) {
        int stepCount = 0;
        int i = (start + CardsMax - 1) % CardsMax;
        while (_cards[i] == null) {
            Debug.Log(i);
            
            if (stepCount == CardsMax - 1) return -1;

            stepCount++;
            i = (i + CardsMax - 1) % CardsMax;
        }

        return i;
    }

    public void RemoveCard(int i) {
        _cards[i] = null;
        Destroy(cardUIGameObjects[i]);
        cardUIGameObjects[i] = null;
    }
}