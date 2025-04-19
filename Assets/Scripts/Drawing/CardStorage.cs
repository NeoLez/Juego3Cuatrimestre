using System;
using System.Collections.Generic;
using UnityEngine;

public class CardStorage : MonoBehaviour {
    private readonly List<SpellSO> _spells = new ();
    public event Action<byte> CardRemoved;
    public event Action<SpellSO> CardAdded;

    public void RemoveCard(byte n) {
        n--;
        if (n < _spells.Count) {
            _spells.RemoveAt(n);
            CardRemoved?.Invoke(n);
        }
    }

    public void AddCard(SpellSO spell) {
        _spells.Add(spell);
        CardAdded?.Invoke(spell);
    }

    public void UseCard(byte n) {
        n--;
        if (n < _spells.Count) {
            _spells[n].RunSpell();
            
            _spells.RemoveAt(n);
            CardRemoved?.Invoke(n);
        }
    }
}