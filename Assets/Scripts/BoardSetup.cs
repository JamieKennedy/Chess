using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class BoardSetup : MonoBehaviour {

    public GameObject tilePrefab;
    public GameObject piecePrefab;

    private Vector3 bottomLeft = new Vector3(-3.5f, -3.5f, 0f);

    private GameObject[,] board = new GameObject[8, 8];

    Sprites sprites;

    public GameObject playerPrefab;

    private PlayerManager playerManager;



    public void Awake() {
        sprites = gameObject.GetComponent<Sprites>();
        playerManager = gameObject.GetComponent<PlayerManager>();
    }

    public GameObject[,] GetBoard() {
        return board;
    }

    public void InitPlayers() {
        playerManager.player1 = Instantiate(playerPrefab);
        playerManager.player1Component = playerManager.player1.GetComponent<Players>();
        playerManager.player1Component.playerColour = Board.Colours.white;
        playerManager.player1Component.playerNum = Board.Players.player1;

        playerManager.player2 = Instantiate(playerPrefab);
        playerManager.player2Component = playerManager.player2.GetComponent<Players>();
        playerManager.player2Component.playerColour = Board.Colours.black;
        playerManager.player2Component.playerNum = Board.Players.player2;
    }

    public void InitBoard() {
        GameObject currentTile;
        Tile tileComponent;
        SpriteRenderer sr;

        for (int y = 0; y < 8; y++) {
            for (int x = 0; x < 8; x++) {
                Vector3 pos = bottomLeft + new Vector3(x, y, 0);
                currentTile = Instantiate(tilePrefab, pos, Quaternion.identity);
                tileComponent = currentTile.GetComponent<Tile>();

                board[y, x] = currentTile;
                tileComponent.pos = new Vector2(x, y);

                currentTile.transform.parent = this.gameObject.transform;
                sr = currentTile.GetComponent<SpriteRenderer>();
                SetTileSprite(x, y, ref sr);
            }
        }
    }

    public void InitPieces() {
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

                tileAtPos.GetComponent<Tile>().piece = piece;

                pieceComponent.colour = Board.Colours.white;
                pieceComponent.parentTile = tileAtPos;

                if (y == 1) {
                    pieceComponent.type = Board.PieceType.pawn;
                    SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                } else {
                    if (x == 0 || x == 7) {
                        pieceComponent.type = Board.PieceType.rook;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    } else if (x == 1 || x == 6) {
                        pieceComponent.type = Board.PieceType.knight;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    } else if (x == 2 || x == 5) {
                        pieceComponent.type = Board.PieceType.bishop;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    } else if (x == 3) {
                        pieceComponent.type = Board.PieceType.queen;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    } else {
                        pieceComponent.type = Board.PieceType.king;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                        playerManager.player1Component.kingPiece = piece;
                    }
                }
                playerManager.player1Component.alivePieces.Add(piece);
                pieceComponent.parentPlayer = playerManager.player1;
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

                pieceComponent.colour = Board.Colours.black;
                pieceComponent.parentTile = tileAtPos;

                if (y == 6) {
                    pieceComponent.type = Board.PieceType.pawn;
                    SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                } else {
                    if (x == 0 || x == 7) {
                        pieceComponent.type = Board.PieceType.rook;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    } else if (x == 1 || x == 6) {
                        pieceComponent.type = Board.PieceType.knight;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    } else if (x == 2 || x == 5) {
                        pieceComponent.type = Board.PieceType.bishop;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    } else if (x == 3) {
                        pieceComponent.type = Board.PieceType.queen;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                    } else {
                        pieceComponent.type = Board.PieceType.king;
                        SetPieceSprite(pieceComponent.type, pieceComponent.colour, ref sr);
                        playerManager.player2Component.kingPiece = piece;
                    }
                }
                playerManager.player2Component.alivePieces.Add(piece);
                pieceComponent.parentPlayer = playerManager.player2;
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


    private void SetPieceSprite(Board.PieceType type, Board.Colours colour, ref SpriteRenderer sr) {
        switch (type) {
            case Board.PieceType.king:
                switch (colour) {
                    case Board.Colours.black:
                        sr.sprite = sprites.kingBlack;
                        break;
                    case Board.Colours.white:
                        sr.sprite = sprites.kingWhite;
                        break;
                }
                break;
            case Board.PieceType.queen:
                switch (colour) {
                    case Board.Colours.black:
                        sr.sprite = sprites.queenBlack;
                        break;
                    case Board.Colours.white:
                        sr.sprite = sprites.queenWhite;
                        break;
                }
                break;
            case Board.PieceType.knight:
                switch (colour) {
                    case Board.Colours.black:
                        sr.sprite = sprites.knightBlack;
                        break;
                    case Board.Colours.white:
                        sr.sprite = sprites.knightWhite;
                        break;
                }
                break;
            case Board.PieceType.bishop:
                switch (colour) {
                    case Board.Colours.black:
                        sr.sprite = sprites.bishopBlack;
                        break;
                    case Board.Colours.white:
                        sr.sprite = sprites.bishopWhite;
                        break;
                }
                break;
            case Board.PieceType.rook:
                switch (colour) {
                    case Board.Colours.black:
                        sr.sprite = sprites.rookBlack;
                        break;
                    case Board.Colours.white:
                        sr.sprite = sprites.rookWhite;
                        break;
                }
                break;
            case Board.PieceType.pawn:
                switch (colour) {
                    case Board.Colours.black:
                        sr.sprite = sprites.pawnBlack;
                        break;
                    case Board.Colours.white:
                        sr.sprite = sprites.pawnWhite;
                        break;
                }
                break;
        }
    }
}
