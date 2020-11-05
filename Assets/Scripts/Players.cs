using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour {

    public enum PlayerType {
        Human,
        CPU
    }

    public Board.Colours playerColour;
    public Board.Players playerNum;

    public List<GameObject> alivePieces = new List<GameObject>();
    public List<GameObject> deadPieces = new List<GameObject>();

    public PlayerType playerType = PlayerType.Human;

    public bool currentTurn;

    private Vector2 pos;
    private Board.PieceType pieceType;
    private Piece pieceComponent;
    private Moves moves;

    public GameObject kingPiece;
    private Piece kingComponent;
    private Moves kingMoves;
    private Vector2 kingPos;

    public HashSet<Vector2> GetAllMoves() {
        HashSet<Vector2> allMoves = new HashSet<Vector2>();
        List<Vector2> pieceMoves = new List<Vector2>();

        foreach (GameObject piece in alivePieces) {
            pieceComponent = piece.GetComponent<Piece>();
            moves = piece.GetComponent<Moves>();
            pos = pieceComponent.parentTile.GetComponent<Tile>().pos;
            pieceType = pieceComponent.type;

            switch (pieceType) {
                case Board.PieceType.king:
                    pieceMoves = moves.GetKingMoves(pos);
                    break;
                case Board.PieceType.queen:
                    pieceMoves = moves.GetQueenMoves(pos);
                    break;
                case Board.PieceType.knight:
                    pieceMoves = moves.GetKnightMoves(pos);
                    break;
                case Board.PieceType.bishop:
                    pieceMoves = moves.GetBishopMoves(pos);
                    break;
                case Board.PieceType.rook:
                    pieceMoves = moves.GetRookMoves(pos);
                    break;
                case Board.PieceType.pawn:
                    pieceMoves = moves.GetPawnMoves(pos);
                    break;
            }

            foreach (Vector2 move in pieceMoves) {
                allMoves.Add(move);
            }
        }

        return allMoves;
    }

    public HashSet<Vector2> GetKingMoves() {
        if (alivePieces.Contains(kingPiece)) {
            kingComponent = kingPiece.GetComponent<Piece>();
            kingMoves = kingPiece.GetComponent<Moves>();
            kingPos = kingComponent.parentTile.GetComponent<Tile>().pos;


            return new HashSet<Vector2>(kingMoves.GetKingMoves(kingPos));
        } else {
            return new HashSet<Vector2>();
        }

    }

    public void SetCanMove(bool state) {
        currentTurn = state;
        foreach (GameObject piece in alivePieces) {
            pieceComponent = piece.GetComponent<Piece>();
            moves = piece.GetComponent<Moves>();

            pos = pieceComponent.parentTile.GetComponent<Tile>().pos;
            pieceType = pieceComponent.type;

            pieceComponent.canMove = state;
            moves.GetMoves(pos, pieceType);
        }
    }


}
