using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {

    public bool isBeingDragged = false;
    private Vector2 mousePos;
    private Vector3 defaultScale;
    private SpriteRenderer sr;

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
        sr = gameObject.GetComponent<SpriteRenderer>();

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
        if (piece.canMove) {
            if (Input.GetMouseButtonDown(0)) {
                sr.sortingOrder += 1;
                isBeingDragged = true;
                moves.HighlightTiles();
                gameObject.transform.localScale = defaultScale * 1.2f;
            }
        }
    }

    private void OnMouseUp() {
        if (piece.canMove) {
            sr.sortingOrder -= 1;
            isBeingDragged = false;
            moves.ResetTileHighlights();
            gameObject.transform.localScale = defaultScale;
            board.MovePiece(piece.parentTile, tilePieceIsOver, gameObject, IsValidMove());
        }
    }

    private void SetTilePieceIsOver() {
        GameObject tile;
        Collider col;
        for (int y = 0; y < 8; y++) {
            for (int x = 0; x < 8; x++) {
                tile = board.GetTile(new Vector2(x, y));
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
