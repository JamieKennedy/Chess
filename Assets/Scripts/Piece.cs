﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    public Board.PieceType type;
    public Board.Colours colour;

    public GameObject parentTile;

    public bool canMove = true;

    public int moveCount;

    public GameObject parentPlayer;
}
