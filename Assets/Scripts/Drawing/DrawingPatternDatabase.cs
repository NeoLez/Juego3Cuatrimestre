using System.Collections.Generic;
using Optional;
using UnityEngine;

public static class DrawingPatternDatabase {
    private static readonly DrawingPatternSO[] Patterns = Resources.LoadAll<DrawingPatternSO>("Patterns");
    private static readonly HashSet<DrawingPatternSO> UnlockedPatterns = new();

    public static Option<CardInfoSO> GetSpellFromDrawing(Drawing drawing) {
        foreach (var pattern in UnlockedPatterns) {
            if (pattern.drawing.Equals(drawing)) {
                return pattern.cardInfo.SomeNotNull();
            }
        }

        return Option.None<CardInfoSO>();
    }

    public static void UnlockSpell(DrawingPatternSO drawingPattern) {
        UnlockedPatterns.Add(drawingPattern);
    }
}