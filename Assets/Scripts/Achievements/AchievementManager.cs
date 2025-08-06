using System;
using System.Collections.Generic;
using Conditions;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Achievements {
    public static class AchievementManager {
        private static readonly float SecondsBetweenChecks = 1.5f;
        private static float _lastTimeChecked = Single.NegativeInfinity;
        private static List<Achievement> _achievements = new();
        public static void CheckAchievements() {
            if (Time.time - _lastTimeChecked < 0.5f) return;
            foreach (var achievement in _achievements) {
                _lastTimeChecked = Time.time;
                
                if (achievement.Evaluate()) {
                    Debug.Log(achievement.Name);
                }
            }
        }

        public static readonly Achievement USE_FREEZE_SPELL = new (
            "Use Freeze Spell", 
            new LeafConditionGeneric<int>(Facts.Facts.TOTAL_FREEZE_USES, num => num >= 1)
            );
        public static readonly Achievement USE_FIRE_SPELL = new (
            "Use Freeze Spell", 
            new LeafConditionGeneric<int>(Facts.Facts.TOTAL_FIRE_USES, num => num >= 1)
        );
        public static readonly Achievement USE_DASH_SPELL = new (
            "Use Freeze Spell", 
            new LeafConditionGeneric<int>(Facts.Facts.TOTAL_DASH_USES, num => num >= 1)
        );
        public static readonly Achievement DIE_ONCE = new (
            "Use Freeze Spell", 
            new LeafConditionGeneric<int>(Facts.Facts.TOTAL_DEATHS, num => num >= 1)
        );
        public static readonly Achievement COMPLETE_GAME = new (
            "Use Freeze Spell", 
            new LeafConditionGeneric<int>(Facts.Facts.TOTAL_GAME_COMPLETIONS, num => num >= 1)
        );
        
        static AchievementManager() {
            _achievements.Add(USE_FREEZE_SPELL);
            _achievements.Add(USE_DASH_SPELL);
            _achievements.Add(USE_FIRE_SPELL);
            _achievements.Add(DIE_ONCE);
            _achievements.Add(COMPLETE_GAME);
	    }
    }
}
