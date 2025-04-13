using System;
using System.Collections.Generic;
using UnityEngine;

public class CardStorage : MonoBehaviour {
    private readonly List<SpellSO> spells = new ();
    public event Action<byte> CardRemoved;
    public event Action<SpellSO> CardAdded;

    public void RemoveCard(byte n) {
        n--;
        if (n < spells.Count) {
            spells.RemoveAt(n);
            CardRemoved?.Invoke(n);
        }
    }

    public void AddCard(SpellSO spell) {
        spells.Add(spell);
        CardAdded?.Invoke(spell);
    }

    public void UseCard(byte n) {
        n--;
        if (n < spells.Count) {
            spells[n].RunSpell();
            
            spells.RemoveAt(n);
            CardRemoved?.Invoke(n);
        }
    }
}