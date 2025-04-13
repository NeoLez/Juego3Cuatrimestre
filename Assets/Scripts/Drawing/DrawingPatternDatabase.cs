using Optional;
using UnityEngine;

public static class DrawingPatternDatabase {
    private static readonly DrawingPatternSO[] Patterns = Resources.LoadAll<DrawingPatternSO>("Patterns");

    public static Option<SpellSO> GetSpellFromDrawing(Drawing drawing) {
        foreach (var pattern in Patterns) {
            if (pattern.drawing.Equals(drawing)) {
                return pattern.spell.Some();
            }
        }

        return Option.None<SpellSO>();
    }
}