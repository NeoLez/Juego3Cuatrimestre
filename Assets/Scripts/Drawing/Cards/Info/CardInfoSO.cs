using UnityEngine;

public abstract class CardInfoSO : ScriptableObject {
    [SerializeField] public Sprite icon;
    [SerializeField] public byte id;
    [SerializeField] public string spellName;
    [SerializeField] public byte inkCost;
    [SerializeField] public byte maxUses;

    public abstract Card GetCard(byte position);
}
