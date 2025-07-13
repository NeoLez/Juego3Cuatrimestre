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
            foreach (var achievement in _achievements) {
                if (Time.time - _lastTimeChecked < 0.5f) return;
                _lastTimeChecked = Time.time;
                
                if (achievement.Evaluate()) {
                    Debug.Log(achievement.Name);
                }
            }
        }

        public static readonly Achievement JUMP_3_TIMES = new (
            "Jump 3 Times", 
            new LeafConditionGeneric<int>(Facts.Facts.TOTAL_JUMPS, num => num >= 3)
            );

        public static readonly Achievement WALKED_3_SECONDS_AND_JUMPED_5_TIMES = new (
            "Walked 3 seconds and jumped 5 times",
            new OrCondition(
                new AndCondition(
                    new LeafConditionGeneric<int>(Facts.Facts.TOTAL_JUMPS, val => val >= 5), 
                    new LeafConditionGeneric<int>(Facts.Facts.TOTAL_DASH_USES_SELF, val => val >= 3)
                ),
                new LeafConditionGeneric<float>(Facts.Facts.TOTAL_WALK_TIME, val => val >= 3)
            )
        );
        
        static AchievementManager() {
            //_achievements.Add(JUMP_3_TIMES);
            _achievements.Add(WALKED_3_SECONDS_AND_JUMPED_5_TIMES);
	}
    }
}
