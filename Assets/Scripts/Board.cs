using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class Board : MonoBehaviour {
    public enum PieceType {
        king,
        queen,
        knight,
        bishop,
        rook,
        pawn
    }

    public enum Colours {
        black,
        white
    }

    public GameObject[,] board = new GameObject[8, 8];

    private int blackDeathCount, whiteDeathCount;
    private Vector3 blackDeathPos = new Vector3(-4.25f, 4.3f, 0f);
    private Vector3 whiteDeathPos = new Vector3(-4.25f, -4.3f, 0f);



    public void Start() {
        BoardSetup setup = this.gameObject.GetComponent<BoardSetup>();
        setup.InitBoard();
        setup.InitPieces();

        board = setup.GetBoard();
    }

    public void MovePiece(GameObject tileA, GameObject tileB, GameObject piece) {
        Tile tileAComponent = tileA.GetComponent<Tile>();
        Tile tileBComponent = tileB.GetComponent<Tile>();
        GameObject tileBPiece = tileBComponent.piece;

        if (piece) {
            Piece tileAPieceComponent = piece.GetComponent<Piece>();
            if (tileBPiece) {
                Piece tileBPieceComponent = tileBPiece.GetComponent<Piece>();
                if (tileBPieceComponent.colour == tileAPieceComponent.colour) {
                    piece.transform.position = tileA.transform.position;
                } else {
                    tileBPieceComponent.parentTile = null;
                    KillPiece(tileBPiece);


                    tileAComponent.piece = null;
                    tileBComponent.piece = piece;
                    tileAPieceComponent.parentTile = tileB;

                    piece.transform.position = tileB.transform.position;
                    piece.transform.parent = tileB.transform;
                }




            } else {
                tileBComponent.piece = piece;
                tileAComponent.piece = null;
                tileAPieceComponent.parentTile = tileB;

                piece.transform.position = tileB.transform.position;
                piece.transform.parent = tileB.transform;
            }

        }
    }

    private void KillPiece(GameObject piece) {
        Piece pieceComponent = piece.GetComponent<Piece>();
        pieceComponent.canMove = false;
        if (pieceComponent.colour == Colours.white) {
            whiteDeathCount++;
            piece.transform.localScale *= 0.5f;
            piece.transform.position = whiteDeathPos + new Vector3(0.5f * whiteDeathCount, 0f, 0f);
        } else {
            blackDeathCount++;
            piece.transform.localScale *= 0.5f;
            piece.transform.position = blackDeathPos + new Vector3(0.5f * blackDeathCount, 0f, 0f);
        }
    }

    public GameObject GetTile(int x, int y) {
        return board[y, x];
    }
}
