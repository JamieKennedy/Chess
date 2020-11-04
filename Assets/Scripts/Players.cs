using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour {

    public enum PlayerType {
        Human,
        CPU
    }

    public Board.Colours playerColour;

    public List<GameObject> alivePieces = new List<GameObject>();
    public List<GameObject> deadPieces = new List<GameObject>();

    public PlayerType playerType = PlayerType.Human;

    public bool currentTurn;

    public void SetCanMove(bool state) {
        currentTurn = state;
        foreach (GameObject piece in alivePieces) {
            piece.GetComponent<Piece>().canMove = state;
        }
    }


}
