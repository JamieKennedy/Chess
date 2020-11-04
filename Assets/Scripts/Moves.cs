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
                moves = GetKingMoves(parentTileComponent.pos);
                break;
            case Board.PieceType.queen:
                moves = GetQueenMoves(parentTileComponent.pos);
                break;
            case Board.PieceType.knight:
                moves = GetKnightMoves(parentTileComponent.pos);
                break;
            case Board.PieceType.bishop:
                moves = GetBishopMoves(parentTileComponent.pos);
                break;
            case Board.PieceType.rook:
                moves = GetRookMoves(parentTileComponent.pos);
                break;
            case Board.PieceType.pawn:
                moves = GetPawnMoves(parentTileComponent.pos);
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

    private List<GameObject> GetKingMoves(Vector2 pos) {
        List<GameObject> kingMoves = new List<GameObject>();
        Vector2 kingMove;

        Vector2[] deltas = new Vector2[] {
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(1, -1),
            new Vector2(0, -1),
            new Vector2(-1, -1),
            new Vector2(-1, 0),
            new Vector2(-1, 1)
        };

        for (int i = 0; i < 8; i++) {
            kingMove = pos + deltas[i];


            if (IsInBounds(kingMove) && IsFree(kingMove)) {
                kingMoves.Add(board.GetTile(kingMove));
            }
        }

        return kingMoves;
    }

    private List<GameObject> GetKnightMoves(Vector2 pos) {
        List<GameObject> knightMoves = new List<GameObject>();
        Vector2 knightMove;

        Vector2[] deltas = new Vector2[] {
            new Vector2(1, 2),
            new Vector2(2, 1),
            new Vector2(2, -1),
            new Vector2(1, -2),
            new Vector2(-1, -2),
            new Vector2(-2, -1),
            new Vector2(-2, 1),
            new Vector2(-1, 2)
        };

        for (int i = 0; i < 8; i++) {
            knightMove = pos + deltas[i];


            if (IsInBounds(knightMove) && IsFree(knightMove)) {
                knightMoves.Add(board.GetTile(knightMove));
            }
        }

        return knightMoves;
    }

    private List<GameObject> GetRookMoves(Vector2 pos) {
        List<GameObject> rookMoves = new List<GameObject>();
        Vector2 increment;
        GameObject pieceAtTile;
        Piece pieceAtTileComponent;

        // Look Up
        increment = Vector2.zero;
        while (true) {
            increment.y++;

            if (IsInBounds(pos + increment) && IsFree(pos + increment)) {
                if (pieceAtTile = GetPieceAtTile(pos + increment)) {
                    if (!GetColour(pieceAtTile).Equals(piece.colour)) {
                        rookMoves.Add(board.GetTile(pos + increment));
                        break;
                    }
                } else {
                    rookMoves.Add(board.GetTile(pos + increment));
                }

            } else {
                break;
            }
        }

        // Look Down
        increment = Vector2.zero;
        while (true) {
            increment.y--;

            if (IsInBounds(pos + increment) && IsFree(pos + increment)) {
                if (pieceAtTile = GetPieceAtTile(pos + increment)) {
                    if (!GetColour(pieceAtTile).Equals(piece.colour)) {
                        rookMoves.Add(board.GetTile(pos + increment));
                        break;
                    }
                } else {
                    rookMoves.Add(board.GetTile(pos + increment));
                }
            } else {
                break;
            }

        }

        // Look Left
        increment = Vector2.zero;
        while (true) {
            increment.x--;

            if (IsInBounds(pos + increment) && IsFree(pos + increment)) {
                if (pieceAtTile = GetPieceAtTile(pos + increment)) {
                    if (!GetColour(pieceAtTile).Equals(piece.colour)) {
                        rookMoves.Add(board.GetTile(pos + increment));
                        break;
                    }
                } else {
                    rookMoves.Add(board.GetTile(pos + increment));
                }
            } else {
                break;
            }

        }

        // Look Right
        increment = Vector2.zero;
        while (true) {
            increment.x++;

            if (IsInBounds(pos + increment) && IsFree(pos + increment)) {
                if (pieceAtTile = GetPieceAtTile(pos + increment)) {
                    pieceAtTileComponent = piece.GetComponent<Piece>();
                    if (!GetColour(pieceAtTile).Equals(piece.colour)) {
                        rookMoves.Add(board.GetTile(pos + increment));
                        break;
                    }
                } else {
                    rookMoves.Add(board.GetTile(pos + increment));
                }
            } else {
                break;
            }

        }

        return rookMoves;

    }

    private List<GameObject> GetBishopMoves(Vector2 pos) {
        List<GameObject> bishopMoves = new List<GameObject>();
        Vector2 increment;
        GameObject pieceAtTile;
        Piece pieceAtTileComponent;

        Vector2 upRight = new Vector2(1, 1);
        Vector2 downRight = new Vector2(1, -1);
        Vector2 upLeft = new Vector2(-1, 1);
        Vector2 downLeft = new Vector2(-1, -1);


        // Look Diagonal Up Left
        increment = Vector2.zero;
        while (true) {
            increment += upLeft;

            if (IsInBounds(pos + increment) && IsFree(pos + increment)) {
                if (pieceAtTile = GetPieceAtTile(pos + increment)) {
                    if (!GetColour(pieceAtTile).Equals(piece.colour)) {
                        bishopMoves.Add(board.GetTile(pos + increment));
                        break;
                    }
                } else {
                    bishopMoves.Add(board.GetTile(pos + increment));
                }

            } else {
                break;
            }
        }

        // Look Diagonal Down Left
        increment = Vector2.zero;
        while (true) {
            increment += downLeft;

            if (IsInBounds(pos + increment) && IsFree(pos + increment)) {
                if (pieceAtTile = GetPieceAtTile(pos + increment)) {
                    if (!GetColour(pieceAtTile).Equals(piece.colour)) {
                        bishopMoves.Add(board.GetTile(pos + increment));
                        break;
                    }
                } else {
                    bishopMoves.Add(board.GetTile(pos + increment));
                }
            } else {
                break;
            }

        }

        // Look Diagonal Down Right
        increment = Vector2.zero;
        while (true) {
            increment += downRight;

            if (IsInBounds(pos + increment) && IsFree(pos + increment)) {
                if (pieceAtTile = GetPieceAtTile(pos + increment)) {
                    if (!GetColour(pieceAtTile).Equals(piece.colour)) {
                        bishopMoves.Add(board.GetTile(pos + increment));
                        break;
                    }
                } else {
                    bishopMoves.Add(board.GetTile(pos + increment));
                }
            } else {
                break;
            }

        }

        // Look Diagonal Up Right
        increment = Vector2.zero;
        while (true) {
            increment += upRight;

            if (IsInBounds(pos + increment) && IsFree(pos + increment)) {
                if (pieceAtTile = GetPieceAtTile(pos + increment)) {
                    pieceAtTileComponent = piece.GetComponent<Piece>();
                    if (!GetColour(pieceAtTile).Equals(piece.colour)) {
                        bishopMoves.Add(board.GetTile(pos + increment));
                        break;
                    }
                } else {
                    bishopMoves.Add(board.GetTile(pos + increment));
                }
            } else {
                break;
            }

        }

        return bishopMoves;

    }

    private List<GameObject> GetQueenMoves(Vector2 pos) {
        return GetBishopMoves(pos).Concat(GetRookMoves(pos)).ToList();
    }

    private List<GameObject> GetPawnMoves(Vector2 pos) {
        List<GameObject> pawnMoves = new List<GameObject>();
        GameObject diagonalPiece;

        Vector2 upRight = new Vector2(1, 1);
        Vector2 downRight = new Vector2(1, -1);
        Vector2 upLeft = new Vector2(-1, 1);
        Vector2 downLeft = new Vector2(-1, -1);


        switch (piece.colour) {
            case Board.Colours.black:
                if (IsInBounds(pos + Vector2.down) && !GetPieceAtTile(pos + Vector2.down)) {
                    pawnMoves.Add(board.GetTile(pos + Vector2.down));
                }

                if (piece.moveCount == 0 && !GetPieceAtTile(pos + Vector2.down)) {
                    if (IsInBounds(pos + (Vector2.down * 2)) && !GetPieceAtTile(pos + (Vector2.down * 2))) {
                        pawnMoves.Add(board.GetTile(pos + (Vector2.down * 2)));
                    }
                }

                if (diagonalPiece = GetPieceAtTile(pos + downLeft)) {
                    if (!GetColour(diagonalPiece).Equals(piece.colour)) {
                        pawnMoves.Add(board.GetTile(pos + downLeft));
                    }
                }

                if (diagonalPiece = GetPieceAtTile(pos + downRight)) {
                    if (!GetColour(diagonalPiece).Equals(piece.colour)) {
                        pawnMoves.Add(board.GetTile(pos + downRight));
                    }
                }
                break;
            case Board.Colours.white:
                if (IsInBounds(pos + Vector2.up) && !GetPieceAtTile(pos + Vector2.up)) {
                    pawnMoves.Add(board.GetTile(pos + Vector2.up));
                }

                if (piece.moveCount == 0 && !GetPieceAtTile(pos + Vector2.up)) {
                    if (IsInBounds(pos + (Vector2.up * 2)) && !GetPieceAtTile(pos + (Vector2.up * 2))) {
                        pawnMoves.Add(board.GetTile(pos + (Vector2.up * 2)));
                    }
                }

                if (diagonalPiece = GetPieceAtTile(pos + upLeft)) {
                    if (!GetColour(diagonalPiece).Equals(piece.colour)) {
                        pawnMoves.Add(board.GetTile(pos + upLeft));
                    }
                }

                if (diagonalPiece = GetPieceAtTile(pos + upRight)) {
                    if (!GetColour(diagonalPiece).Equals(piece.colour)) {
                        pawnMoves.Add(board.GetTile(pos + upRight));
                    }
                }
                break;
            default:
                break;
        }

        return pawnMoves;
    }



    private bool IsInBounds(Vector2 pos) {
        if (pos.x >= 0 && pos.x < 8) {
            if (pos.y >= 0 && pos.y < 8) {
                return true;
            }
            return false;
        }

        return false;
    }

    private bool IsFree(Vector2 pos) {
        GameObject tile = board.GetTile(pos);
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

    private GameObject GetPieceAtTile(Vector2 pos) {
        if (IsInBounds(pos)) {
            GameObject tile = board.GetTile(pos);
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
