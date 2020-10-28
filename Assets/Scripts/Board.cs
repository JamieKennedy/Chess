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


}
