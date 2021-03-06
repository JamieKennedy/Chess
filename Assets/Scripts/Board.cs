﻿using System.Collections;
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

    private GameObject dead;

    private int blackDeathCount, whiteDeathCount;
    private Vector3 blackDeathPos = new Vector3(-4.25f, 4.3f, 0f);
    private Vector3 whiteDeathPos = new Vector3(-4.25f, -4.3f, 0f);

    private PlayerManager playerManager;



    public void Start() {
        dead = GameObject.FindGameObjectWithTag("Dead");
        BoardSetup setup = this.gameObject.GetComponent<BoardSetup>();
        playerManager = this.gameObject.GetComponent<PlayerManager>();
        setup.InitBoard();
        setup.InitPlayers();
        setup.InitPieces();




        board = setup.GetBoard();


        playerManager.SetInitialTurnState();


    }

    public bool MovePiece(GameObject tileA, GameObject tileB, GameObject piece, bool valid) {
        Tile tileAComponent = tileA.GetComponent<Tile>();
        Tile tileBComponent = tileB.GetComponent<Tile>();
        GameObject tileBPiece = tileBComponent.piece;

        if (piece) {
            Piece tileAPieceComponent = piece.GetComponent<Piece>();
            if (valid) {
                if (tileBPiece) {
                    Piece tileBPieceComponent = tileBPiece.GetComponent<Piece>();
                    if (tileBPieceComponent.colour == tileAPieceComponent.colour) {
                        piece.transform.position = tileA.transform.position;
                        return false;
                    } else {
                        tileBPieceComponent.parentTile = null;
                        KillPiece(tileBPiece);

                        tileAComponent.piece = null;
                        tileBComponent.piece = piece;
                        tileAPieceComponent.parentTile = tileB;

                        piece.transform.position = tileB.transform.position;
                        piece.transform.parent = tileB.transform;

                        tileAPieceComponent.moveCount++;

                        playerManager.SwapPlayerState();
                        return true;
                    }
                } else {
                    tileBComponent.piece = piece;
                    tileAComponent.piece = null;
                    tileAPieceComponent.parentTile = tileB;

                    piece.transform.position = tileB.transform.position;
                    piece.transform.parent = tileB.transform;

                    tileAPieceComponent.moveCount++;

                    playerManager.SwapPlayerState();
                    return true;
                }
            } else {
                piece.transform.position = tileAPieceComponent.parentTile.transform.position;
                return false;
            }
        } else {
            return false;
        }
    }

    private void KillPiece(GameObject piece) {
        Piece pieceComponent = piece.GetComponent<Piece>();
        piece.transform.parent = dead.transform;
        pieceComponent.canMove = false;
        if (pieceComponent.colour == Colours.white) {
            whiteDeathCount++;
            piece.transform.localScale *= 0.5f;
            piece.transform.position = whiteDeathPos + new Vector3(0.5f * whiteDeathCount, 0f, 0f);
        } else {
            blackDeathCount++;
            piece.transform.localScale *= 0.5f;
            piece.transform.position = blackDeathPos + new Vector3(0.5f * blackDeathCount, 0f, 0f);
        }
    }

    public void FinishedTurn() {

    }

    public GameObject GetTile(Vector2 pos) {
        return board[(int)pos.y, (int)pos.x];
    }
}
