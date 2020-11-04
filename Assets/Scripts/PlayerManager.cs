using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;

    public Players player1Component;
    public Players player2Component;

    public void SwapPlayerState() {
        if (player1Component.currentTurn) {
            player2Component.SetCanMove(true);
            player1Component.SetCanMove(false);
        } else {
            player2Component.SetCanMove(false);
            player1Component.SetCanMove(true);
        }
    }

    public void SetInitialTurnState() {
        if (GetRandom() > 0.5) {
            player1Component.SetCanMove(true);
            player2Component.SetCanMove(false);
        } else {
            player1Component.SetCanMove(false);
            player2Component.SetCanMove(true);
        }
    }

    // Returns a random double between 0 and 1
    private double GetRandom() {
        System.Random rnd = new System.Random();
        double n = rnd.NextDouble();

        return n;
    }
}
