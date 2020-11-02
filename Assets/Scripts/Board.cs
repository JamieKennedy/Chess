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


    public void Start() {
        BoardSetup setup = this.gameObject.GetComponent<BoardSetup>();
        setup.InitBoard();
        setup.InitPieces();

        board = setup.GetBoard();
    }

    public void MovePiece(GameObject tileA, GameObject tileB) {
        Tile tileAComponent = tileA.GetComponent<Tile>();
        Tile tileBComponent = tileA.GetComponent<Tile>();
        GameObject tileAPiece = tileAComponent.piece;
        GameObject tileBPiece = tileBComponent.piece;

        if (tileAPiece) {
            Piece tileAPieceComponent = tileAPiece.GetComponent<Piece>();
            if (tileBPiece) {
                Piece tileBPieceComponent = tileBPiece.GetComponent<Piece>();

                tileBPieceComponent.parentTile = null;
                tileBPiece.transform.position = new Vector3(0, 0, 0);

                tileAComponent.piece = null;
                tileBComponent.piece = tileAPiece;
                tileAPieceComponent.parentTile = tileB;

                tileAPiece.transform.position = tileB.transform.position;
            } else {
                tileAComponent.piece = null;
                tileBComponent.piece = tileAPiece;
                tileAPieceComponent.parentTile = tileB;

                tileAPiece.transform.position = tileB.transform.position;
            }

        }
    }
}
