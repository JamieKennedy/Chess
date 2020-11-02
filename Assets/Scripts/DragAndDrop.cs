using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {

    public bool isBeingDragged = false;
    private Vector2 mousePos;
    private Vector3 defaultScale;

    public GameObject tilePieceIsOver;
    private GameObject gameManager;
    private Board board;
    private Piece piece;

    private void Start() {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        board = gameManager.GetComponent<Board>();
        piece = gameObject.GetComponent<Piece>();
        defaultScale = gameObject.transform.localScale;
    }

    private void Update() {
        if (piece.canMove) {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (isBeingDragged) {
                SetTilePieceIsOver();
                gameObject.transform.position = mousePos;
                gameObject.transform.localScale = defaultScale * 1.2f;
            } else {
                gameObject.transform.localScale = defaultScale;
            }
        }
    }


    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            isBeingDragged = true;
        }
    }

    private void OnMouseUp() {
        isBeingDragged = false;
        board.MovePiece(piece.parentTile, tilePieceIsOver, gameObject);
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


}
