using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Moves : MonoBehaviour {

    private Piece piece;

    private GameObject gameManager;
    Sprites sprites;
    Board board;

    public List<GameObject> moves = new List<GameObject>();

    private GameObject parentTile;
    private Tile parentTileComponent;

    private void Start() {
        piece = gameObject.GetComponent<Piece>();
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        sprites = gameManager.GetComponent<Sprites>();
        board = gameManager.GetComponent<Board>();


    }

    public void GetMoves() {
        parentTile = piece.parentTile;
        parentTileComponent = parentTile.GetComponent<Tile>();
        switch (piece.type) {
            case Board.PieceType.king:
                moves = GetKingMoves(parentTileComponent.boardPosX, parentTileComponent.boardPosY);
                break;
            case Board.PieceType.queen:
                break;
            case Board.PieceType.knight:
                break;
            case Board.PieceType.bishop:
                break;
            case Board.PieceType.rook:
                break;
            case Board.PieceType.pawn:
                break;
            default:
                break;
        }
    }

    public void HighlightTiles() {
        SpriteRenderer sr;
        if (moves.Count > 0) {
            foreach (GameObject tile in moves) {
                sr = tile.GetComponent<SpriteRenderer>();
                if (sr.sprite.Equals(sprites.tileDark)) {
                    sr.sprite = sprites.tileDarkBorder;
                } else if (sr.sprite.Equals(sprites.tileLight)) {
                    sr.sprite = sprites.tileLightBorder;
                }
            }
        }
    }

    public void ResetTileHighlights() {
        SpriteRenderer sr;
        foreach (GameObject tile in moves) {
            sr = tile.GetComponent<SpriteRenderer>();
            if (sr.sprite.Equals(sprites.tileDarkBorder)) {
                sr.sprite = sprites.tileDark;
            } else if (sr.sprite.Equals(sprites.tileLightBorder)) {
                sr.sprite = sprites.tileLight;
            }
        }
    }

    private List<GameObject> GetKingMoves(int x, int y) {
        List<GameObject> kingMoves = new List<GameObject>();
        int[] kingMove = new int[2];

        int[] xDeltas = new int[] { 0, 1, 1, 1, 0, -1, -1, -1 };
        int[] yDeltas = new int[] { 1, 1, 0, -1, -1, -1, 0, 1 };

        for (int i = 0; i < 8; i++) {
            kingMove[0] = x + xDeltas[i];
            kingMove[1] = y + yDeltas[i];


            if (IsInBounds(kingMove[0], kingMove[1]) && IsFree(kingMove[0], kingMove[1])) {
                kingMoves.Add(board.GetTile(kingMove[0], kingMove[1]));
            }
        }

        return kingMoves;

    }

    private bool IsInBounds(int x, int y) {
        if (x >= 0 && x < 8) {
            if (y >= 0 && y < 8) {
                return true;
            }
            return false;
        }

        return false;
    }

    private bool IsFree(int x, int y) {
        GameObject tile = board.GetTile(x, y);
        Tile tileComponent = tile.GetComponent<Tile>();

        if (tileComponent.piece) {
            Piece tilePiece = tileComponent.piece.GetComponent<Piece>();
            if (tilePiece.colour == piece.colour) {
                return false;
            } else {
                return true;
            }
        } else {
            return true;
        }
    }


}
