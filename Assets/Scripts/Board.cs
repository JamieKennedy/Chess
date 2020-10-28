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


    public GameObject tilePrefab;
    public GameObject piecePrefab;

    public GameObject[,] board = new GameObject[8, 8];

    private Vector3 bottomLeft = new Vector3(-3.5f, -3.5f, 0f);

    private Sprites sprites;

    public void Start() {
        sprites = this.gameObject.GetComponent<Sprites>();
        InitBoard();
        InitPieces();
    }

    private void InitBoard() {
        GameObject currentTile;
        Tile tileComponent;
        SpriteRenderer sr;

        for (int y = 0; y < 8; y++) {
            for (int x = 0; x < 8; x++) {
                Vector3 pos = bottomLeft + new Vector3(x, y, 0);
                currentTile = Instantiate(tilePrefab, pos, Quaternion.identity);
                tileComponent = currentTile.GetComponent<Tile>();

                board[y, x] = currentTile;
                tileComponent.boardPosX = x;
                tileComponent.boardPosY = y;

                currentTile.transform.parent = this.gameObject.transform;
                sr = currentTile.GetComponent<SpriteRenderer>();
                SetTileSprite(x, y, ref sr);
            }
        }
    }

    private void InitPieces() {
        GameObject tileAtPos;
        GameObject piece;
        SpriteRenderer sr;
        Piece pieceComponent;
        // Set bottom pieces
        for (int y = 0; y < 2; y++) {
            for (int x = 0; x < 8; x++) {
                tileAtPos = board[y, x];
                piece = Instantiate(piecePrefab, tileAtPos.transform);
                sr = piece.GetComponent<SpriteRenderer>();
                pieceComponent = piece.GetComponent<Piece>();

                pieceComponent.colour = Colours.white;

                if (y == 1) {
                    pieceComponent.type = PieceType.pawn;
                    SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                } else {
                    if (x == 0 || x == 7) {
                        pieceComponent.type = PieceType.rook;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    } else if (x == 1 || x == 6) {
                        pieceComponent.type = PieceType.knight;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    } else if (x == 2 || x == 5) {
                        pieceComponent.type = PieceType.bishop;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    } else if (x == 3) {
                        pieceComponent.type = PieceType.queen;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    } else {
                        pieceComponent.type = PieceType.king;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    }
                }
            }
        }

        // Set top pieces
        for (int y = 6; y < 8; y++) {
            for (int x = 0; x < 8; x++) {
                tileAtPos = board[y, x];
                piece = Instantiate(piecePrefab, tileAtPos.transform);
                sr = piece.GetComponent<SpriteRenderer>();
                pieceComponent = piece.GetComponent<Piece>();

                tileAtPos.GetComponent<Tile>().piece = piece;

                pieceComponent.colour = Colours.black;
                pieceComponent.parentTile = tileAtPos;

                if (y == 6) {
                    pieceComponent.type = PieceType.pawn;
                    SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                } else {
                    if (x == 0 || x == 7) {
                        pieceComponent.type = PieceType.rook;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    } else if (x == 1 || x == 6) {
                        pieceComponent.type = PieceType.knight;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    } else if (x == 2 || x == 5) {
                        pieceComponent.type = PieceType.bishop;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    } else if (x == 3) {
                        pieceComponent.type = PieceType.queen;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    } else {
                        pieceComponent.type = PieceType.king;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    }
                }
            }
        }
    }

    private void SetTileSprite(int x, int y, ref SpriteRenderer sr) {
        if (IsEven(x) && IsEven(y)) {
            sr.sprite = sprites.tileDark;
        } else if (IsEven(x) && !IsEven(y)) {
            sr.sprite = sprites.tileLight;
        } else if (!IsEven(x) && IsEven(y)) {
            sr.sprite = sprites.tileLight;
        } else {
            sr.sprite = sprites.tileDark;
        }
    }

    private bool IsEven(int num) {
        if (num == 0 || num % 2 == 0) {
            return true;
        }
        return false;
    }


    private void SetPieceSprite(PieceType type, Colours colour, ref SpriteRenderer sr) {
        switch (type) {
            case PieceType.king:
                switch (colour) {
                    case Colours.black:
                        sr.sprite = sprites.kingBlack;
                        break;
                    case Colours.white:
                        sr.sprite = sprites.kingWhite;
                        break;
                }
                break;
            case PieceType.queen:
                switch (colour) {
                    case Colours.black:
                        sr.sprite = sprites.queenBlack;
                        break;
                    case Colours.white:
                        sr.sprite = sprites.queenWhite;
                        break;
                }
                break;
            case PieceType.knight:
                switch (colour) {
                    case Colours.black:
                        sr.sprite = sprites.knightBlack;
                        break;
                    case Colours.white:
                        sr.sprite = sprites.knightWhite;
                        break;
                }
                break;
            case PieceType.bishop:
                switch (colour) {
                    case Colours.black:
                        sr.sprite = sprites.bishopBlack;
                        break;
                    case Colours.white:
                        sr.sprite = sprites.bishopWhite;
                        break;
                }
                break;
            case PieceType.rook:
                switch (colour) {
                    case Colours.black:
                        sr.sprite = sprites.rookBlack;
                        break;
                    case Colours.white:
                        sr.sprite = sprites.rookWhite;
                        break;
                }
                break;
            case PieceType.pawn:
                switch (colour) {
                    case Colours.black:
                        sr.sprite = sprites.pawnBlack;
                        break;
                    case Colours.white:
                        sr.sprite = sprites.pawnWhite;
                        break;
                }
                break;
        }
    }
}
