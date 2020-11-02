using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {

    private Vector2 pos;
    public bool isBeingDragged = false;
    private Vector2 mousePos;
    private Vector3 defaultScale;

    private void Start() {
        defaultScale = gameObject.transform.localScale;
    }

    private void Update() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (isBeingDragged) {
            gameObject.transform.position = mousePos;
            gameObject.transform.localScale = defaultScale * 1.2f;
        } else {
            gameObject.transform.localScale = defaultScale;
        }
    }


    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("clicked");
            isBeingDragged = true;
        }
    }

    private void OnMouseUp() {
        isBeingDragged = false;
    }
}
