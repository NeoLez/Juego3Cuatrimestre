using UnityEngine;

public abstract class SpellSO : ScriptableObject {
    [SerializeField] public Sprite icon;
    [SerializeField] public byte id;
    [SerializeField] public string spellName;
    [SerializeField] public byte inkCost;

    public virtual void RunSpell() {
        Debug.Log(spellName);
    }
}
