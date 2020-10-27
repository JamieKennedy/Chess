using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Board : MonoBehaviour {

    public GameObject tilePrefab;

    public Sprite tileLight, tileDark;

    public GameObject[,] board = new GameObject[8, 8];

    private Vector3 bottomLeft = new Vector3(-3.5f, -3.5f, 0f);

    public void Start() {
        InitBoard();
    }

    private void InitBoard() {
        GameObject currentTile;
        for (int y = 0; y < 8; y++) {
            for (int x = 0; x < 8; x++) {
                Vector3 pos = bottomLeft + new Vector3(x, y, 0);
                currentTile = Instantiate(tilePrefab, pos, Quaternion.identity);
                SpriteRenderer sr = currentTile.GetComponent<SpriteRenderer>();
                SetSprite(x, y, ref sr);
            }
        }
    }

    private void SetSprite(int x, int y, ref SpriteRenderer sr) {
        if (IsEven(x) && IsEven(y)) {
            sr.sprite = tileDark;
        } else if (IsEven(x) && !IsEven(y)) {
            sr.sprite = tileLight;
        } else if (!IsEven(x) && IsEven(y)) {
            sr.sprite = tileLight;
        } else {
            sr.sprite = tileDark;
        }
    }

    private bool IsEven(int num) {
        if (num == 0 || num % 2 == 0) {
            return true;
        }
        return false;
    }

}
