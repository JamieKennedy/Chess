using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                moves = GetQueenMoves(parentTileComponent.boardPosX, parentTileComponent.boardPosY);
                break;
            case Board.PieceType.knight:
                moves = GetKnightMoves(parentTileComponent.boardPosX, parentTileComponent.boardPosY);
                break;
            case Board.PieceType.bishop:
                moves = GetBishopMoves(parentTileComponent.boardPosX, parentTileComponent.boardPosY);
                break;
            case Board.PieceType.rook:
                moves = GetRookMoves(parentTileComponent.boardPosX, parentTileComponent.boardPosY);
                break;
            case Board.PieceType.pawn:
                moves = GetPawnMoves(parentTileComponent.boardPosX, parentTileComponent.boardPosY);
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

    private List<GameObject> GetKnightMoves(int x, int y) {
        List<GameObject> knightMoves = new List<GameObject>();
        int[] knightMove = new int[2];

        int[] xDeltas = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
        int[] yDeltas = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };

        for (int i = 0; i < 8; i++) {
            knightMove[0] = x + xDeltas[i];
            knightMove[1] = y + yDeltas[i];


            if (IsInBounds(knightMove[0], knightMove[1]) && IsFree(knightMove[0], knightMove[1])) {
                knightMoves.Add(board.GetTile(knightMove[0], knightMove[1]));
            }
        }

        return knightMoves;
    }

    private List<GameObject> GetRookMoves(int x, int y) {
        List<GameObject> rookMoves = new List<GameObject>();
        int increment;
        GameObject pieceAtTile;
        Piece pieceAtTileComponent;


        // Look Up
        increment = 0;
        while (true) {
            increment++;

            if (IsInBounds(x, y + increment) && IsFree(x, y + increment)) {
                if (pieceAtTile = GetPieceAtTile(x, y + increment)) {
                    if (!GetColour(pieceAtTile).Equals(piece.colour)) {
                        rookMoves.Add(board.GetTile(x, y + increment));
                        break;
                    }
                } else {
                    rookMoves.Add(board.GetTile(x, y + increment));
                }

            } else {
                break;
            }
        }

        // Look Down
        increment = 0;
        while (true) {
            increment++;

            if (IsInBounds(x, y - increment) && IsFree(x, y - increment)) {
                if (pieceAtTile = GetPieceAtTile(x, y - increment)) {
                    if (!GetColour(pieceAtTile).Equals(piece.colour)) {
                        rookMoves.Add(board.GetTile(x, y - increment));
                        break;
                    }
                } else {
                    rookMoves.Add(board.GetTile(x, y - increment));
                }
            } else {
                break;
            }

        }

        // Look Left
        increment = 0;
        while (true) {
            increment++;

            if (IsInBounds(x - increment, y) && IsFree(x - increment, y)) {
                if (pieceAtTile = GetPieceAtTile(x - increment, y)) {
                    if (!GetColour(pieceAtTile).Equals(piece.colour)) {
                        rookMoves.Add(board.GetTile(x - increment, y));
                        break;
                    }
                } else {
                    rookMoves.Add(board.GetTile(x - increment, y));
                }
            } else {
                break;
            }

        }

        // Look Right
        increment = 0;
        while (true) {
            increment++;

            if (IsInBounds(x + increment, y) && IsFree(x + increment, y)) {
                if (pieceAtTile = GetPieceAtTile(x + increment, y)) {
                    pieceAtTileComponent = piece.GetComponent<Piece>();
                    if (!GetColour(pieceAtTile).Equals(piece.colour)) {
                        rookMoves.Add(board.GetTile(x + increment, y));
                        break;
                    }
                } else {
                    rookMoves.Add(board.GetTile(x + increment, y));
                }
            } else {
                break;
            }

        }

        return rookMoves;

    }

    private List<GameObject> GetBishopMoves(int x, int y) {
        List<GameObject> bishopMoves = new List<GameObject>();
        int increment;
        GameObject pieceAtTile;
        Piece pieceAtTileComponent;


        // Look Diagonal Up Left
        increment = 0;
        while (true) {
            increment++;

            if (IsInBounds(x - increment, y + increment) && IsFree(x - increment, y + increment)) {
                if (pieceAtTile = GetPieceAtTile(x - increment, y + increment)) {
                    if (!GetColour(pieceAtTile).Equals(piece.colour)) {
                        bishopMoves.Add(board.GetTile(x - increment, y + increment));
                        break;
                    }
                } else {
                    bishopMoves.Add(board.GetTile(x - increment, y + increment));
                }

            } else {
                break;
            }
        }

        // Look Diagonal Down Left
        increment = 0;
        while (true) {
            increment++;

            if (IsInBounds(x - increment, y - increment) && IsFree(x - increment, y - increment)) {
                if (pieceAtTile = GetPieceAtTile(x - increment, y - increment)) {
                    if (!GetColour(pieceAtTile).Equals(piece.colour)) {
                        bishopMoves.Add(board.GetTile(x - increment, y - increment));
                        break;
                    }
                } else {
                    bishopMoves.Add(board.GetTile(x - increment, y - increment));
                }
            } else {
                break;
            }

        }

        // Look Diagonal Down Right
        increment = 0;
        while (true) {
            increment++;

            if (IsInBounds(x + increment, y - increment) && IsFree(x + increment, y - increment)) {
                if (pieceAtTile = GetPieceAtTile(x + increment, y - increment)) {
                    if (!GetColour(pieceAtTile).Equals(piece.colour)) {
                        bishopMoves.Add(board.GetTile(x + increment, y - increment));
                        break;
                    }
                } else {
                    bishopMoves.Add(board.GetTile(x + increment, y - increment));
                }
            } else {
                break;
            }

        }

        // Look Diagonal Up Right
        increment = 0;
        while (true) {
            increment++;

            if (IsInBounds(x + increment, y + increment) && IsFree(x + increment, y + increment)) {
                if (pieceAtTile = GetPieceAtTile(x + increment, y + increment)) {
                    pieceAtTileComponent = piece.GetComponent<Piece>();
                    if (!GetColour(pieceAtTile).Equals(piece.colour)) {
                        bishopMoves.Add(board.GetTile(x + increment, y + increment));
                        break;
                    }
                } else {
                    bishopMoves.Add(board.GetTile(x + increment, y + increment));
                }
            } else {
                break;
            }

        }

        return bishopMoves;

    }

    private List<GameObject> GetQueenMoves(int x, int y) {
        return GetBishopMoves(x, y).Concat(GetRookMoves(x, y)).ToList();
    }

    private List<GameObject> GetPawnMoves(int x, int y) {
        List<GameObject> pawnMoves = new List<GameObject>();
        GameObject diagonalPiece;

        switch (piece.colour) {
            case Board.Colours.black:
                if (IsInBounds(x, y - 1) && !GetPieceAtTile(x, y - 1)) {
                    pawnMoves.Add(board.GetTile(x, y - 1));
                }

                if (piece.moveCount == 0 && !GetPieceAtTile(x, y - 1)) {
                    if (IsInBounds(x, y - 2) && !GetPieceAtTile(x, y - 2)) {
                        pawnMoves.Add(board.GetTile(x, y - 2));
                    }
                }

                if (diagonalPiece = GetPieceAtTile(x + 1, y - 1)) {
                    if (!GetColour(diagonalPiece).Equals(piece.colour)) {
                        pawnMoves.Add(board.GetTile(x + 1, y - 1));
                    }
                }

                if (diagonalPiece = GetPieceAtTile(x - 1, y - 1)) {
                    if (!GetColour(diagonalPiece).Equals(piece.colour)) {
                        pawnMoves.Add(board.GetTile(x - 1, y - 1));
                    }
                }
                break;
            case Board.Colours.white:
                if (IsInBounds(x, y + 1) && !GetPieceAtTile(x, y + 1)) {
                    pawnMoves.Add(board.GetTile(x, y + 1));
                }

                if (piece.moveCount == 0 && !GetPieceAtTile(x, y + 1)) {
                    if (IsInBounds(x, y + 2) && !GetPieceAtTile(x, y + 2)) {
                        pawnMoves.Add(board.GetTile(x, y + 2));
                    }
                }

                if (diagonalPiece = GetPieceAtTile(x + 1, y + 1)) {
                    if (!GetColour(diagonalPiece).Equals(piece.colour)) {
                        pawnMoves.Add(board.GetTile(x + 1, y + 1));
                    }
                }

                if (diagonalPiece = GetPieceAtTile(x - 1, y + 1)) {
                    if (!GetColour(diagonalPiece).Equals(piece.colour)) {
                        pawnMoves.Add(board.GetTile(x - 1, y + 1));
                    }
                }
                break;
            default:
                break;
        }

        return pawnMoves;
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

    private GameObject GetPieceAtTile(int x, int y) {
        if (IsInBounds(x, y)) {
            GameObject tile = board.GetTile(x, y);
            Tile tileComponent = tile.GetComponent<Tile>();

            return tileComponent.piece;
        } else {
            return null;
        }
    }

    private Board.Colours GetColour(GameObject piece) {
        Piece tmpPieceComponent = piece.GetComponent<Piece>();
        return tmpPieceComponent.colour;
    }
}
