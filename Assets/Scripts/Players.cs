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

    private Vector2 pos;
    private Board.PieceType pieceType;
    private Piece pieceComponent;

    public GameObject kingPiece;

    public void SetCanMove(bool state) {
        currentTurn = state;
        foreach (GameObject piece in alivePieces) {
            pieceComponent = piece.GetComponent<Piece>();
            pos = pieceComponent.parentTile.GetComponent<Tile>().pos;
            pieceType = pieceComponent.type;

            piece.GetComponent<Piece>().canMove = state;
            piece.GetComponent<Moves>().GetMoves(pos, pieceType);
        }
    }


}
