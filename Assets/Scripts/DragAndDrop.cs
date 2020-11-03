using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {

    public bool isBeingDragged = false;
    private Vector2 mousePos;
    private Vector3 defaultScale;

    public GameObject tilePieceIsOver;
    private GameObject manager;
    private Board board;
    private Piece piece;

    private Moves moves;

    private void Start() {
        manager = GameObject.FindGameObjectWithTag("GameController");
        board = manager.GetComponent<Board>();
        piece = gameObject.GetComponent<Piece>();
        moves = gameObject.GetComponent<Moves>();

        defaultScale = gameObject.transform.localScale;
    }

    private void Update() {
        if (piece.canMove) {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (isBeingDragged) {
                SetTilePieceIsOver();
                gameObject.transform.position = mousePos;
            }
        }
    }


    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            isBeingDragged = true;
            moves.GetMoves();
            moves.HighlightTiles();
            gameObject.transform.localScale = defaultScale * 1.2f;
        }
    }

    private void OnMouseUp() {
        isBeingDragged = false;
        board.MovePiece(piece.parentTile, tilePieceIsOver, gameObject, true);
        moves.ResetTileHighlights();
        gameObject.transform.localScale = defaultScale;
    }

    private void SetTilePieceIsOver() {
        GameObject tile;
        Collider col;
        for (int y = 0; y < 8; y++) {
            for (int x = 0; x < 8; x++) {
                tile = board.GetTile(x, y);
                col = tile.GetComponent<Collider>();

                if (col.bounds.Contains(transform.position)) {
                    tilePieceIsOver = tile;
                }
            }
        }
    }

    private bool IsValidMove() {
        return moves.moves.Contains(tilePieceIsOver);
    }


}
