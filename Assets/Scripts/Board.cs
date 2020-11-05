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

    public enum Players {
        player1,
        player2
    }

    public GameObject[,] board = new GameObject[8, 8];

    private GameObject dead;

    private int blackDeathCount, whiteDeathCount;
    private Vector3 blackDeathPos = new Vector3(-4.25f, 4.3f, 0f);
    private Vector3 whiteDeathPos = new Vector3(-4.25f, -4.3f, 0f);

    private PlayerManager playerManager;

    public HashSet<Vector2> player1AllMoves;
    public HashSet<Vector2> player1KingMoves;

    public List<Vector2> player1AllMovesList;
    public List<Vector2> player1KingMovesList;

    public HashSet<Vector2> player2AllMoves;
    public HashSet<Vector2> player2KingMoves;

    public List<Vector2> player2AllMovesList;
    public List<Vector2> player2KingMovesList;


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
                        FinishedTurn();
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
                    FinishedTurn();
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
            playerManager.player1Component.alivePieces.Remove(piece);
            playerManager.player1Component.deadPieces.Add(piece);
            whiteDeathCount++;
            piece.transform.localScale *= 0.5f;
            piece.transform.position = whiteDeathPos + new Vector3(0.5f * whiteDeathCount, 0f, 0f);
        } else {
            playerManager.player2Component.alivePieces.Remove(piece);
            playerManager.player2Component.deadPieces.Add(piece);
            blackDeathCount++;
            piece.transform.localScale *= 0.5f;
            piece.transform.position = blackDeathPos + new Vector3(0.5f * blackDeathCount, 0f, 0f);
        }
    }

    private void FinishedTurn() {
        player1AllMoves = playerManager.player1Component.GetAllMoves();
        player1KingMoves = playerManager.player1Component.GetKingMoves();

        player1AllMovesList = new List<Vector2>(player1AllMoves);
        player1KingMovesList = new List<Vector2>(player1KingMoves);


        player2AllMoves = playerManager.player2Component.GetAllMoves();
        player2KingMoves = playerManager.player2Component.GetKingMoves();

        player2AllMovesList = new List<Vector2>(player2AllMoves);
        player2KingMovesList = new List<Vector2>(player2KingMoves);

        if (IsCheckmate(Players.player1)) {
            Debug.Log("Player 1 is in checkmate");
        }

        if (IsCheckmate(Players.player2)) {
            Debug.Log("Player 2 is in checkmate");
        }


    }

    private bool IsCheckmate(Players player) {
        if (player == Players.player1) {
            if (player1KingMoves.IsSubsetOf(player2AllMoves) && player1KingMoves.Count != 0) {
                return true;
            } else {
                return false;
            }
        } else {
            if (player2KingMoves.IsSubsetOf(player1AllMoves) && player2KingMoves.Count != 0) {
                return true;
            } else {
                return false;
            }
        }
    }

    public GameObject GetTile(Vector2 pos) {
        return board[(int)pos.y, (int)pos.x];
    }
}
