using System;
using Facts;
using UnityEngine;

namespace Obstacle {
    public class CompleteGameOnTouch : MonoBehaviour{
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject == GameManager.Player) {
                Events.ON_PLAYER_COMPLETED_GAME.Raise(Unit.Default);
            }
        }
    }
}