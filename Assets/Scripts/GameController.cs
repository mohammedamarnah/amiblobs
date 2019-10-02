using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
  [SerializeField]
  public GameObject linePrefab;

  public static Vector3 screenBoundaries;
  public static List<Square> squares = new List<Square>();

  GameObject currentLine;
  LineDrawer lineDrawer;
  Vector2 initialPos, lastPos;

  void Start() {
    screenBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    float width = screenBoundaries.x;
    float height = screenBoundaries.y;
    Vector2 topLeft = new Vector2(-width, height);
    Vector2 topRight = new Vector2(width, height);
    Vector2 bottomLeft = new Vector2(-width, -height);
    Vector2 bottomRight = new Vector2(width, -height);
    Square sq = new Square(topLeft, topRight, bottomLeft, bottomRight);
    squares.Add(sq);
  }

  void Update() {
    if (squares.Count < 1) return;
    if(Input.GetMouseButtonDown(0)) {
      initialPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    if(Input.GetMouseButton(0)) {
      lastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    if (Input.GetMouseButtonUp(0)) {
      if (Vector2.Distance(initialPos, lastPos) > 0.1f) {
        Vector2 dir = new Vector2(lastPos.x - initialPos.x, lastPos.y - initialPos.y);
        bool horizontal = Math.Abs(dir.x) > 0f && Math.Abs(dir.y) <= 0.1f;
        bool vertical = Math.Abs(dir.y) > 0f && Math.Abs(dir.x) <= 0.1f;
        int pos = 0;
        for(int i = 0; i < squares.Count; i++) {
          if (squares[i].Contains(initialPos)) {
            if (horizontal) {
              float right = squares[i].bottomLeft.x;
              float left = squares[i].bottomRight.x;
              Draw(new Vector3(right, initialPos.y, 0));
              Draw(new Vector3(left, initialPos.y, 0));
            } else if (vertical) {
              float top = squares[i].topLeft.y;
              float bottom = squares[i].bottomLeft.y;
              Draw(new Vector3(initialPos.x, top, 0));
              Draw(new Vector3(initialPos.x, bottom, 0));
            }
            pos = i;
            break;
          }
        }
        Square a = new Square();
        Square b = new Square();
        if (horizontal) {
          Vector2 newLeft = new Vector2(squares[pos].bottomLeft.x, initialPos.y);
          Vector2 newRight = new Vector2(squares[pos].bottomRight.x, initialPos.y);
          a = new Square(squares[pos].topLeft, squares[pos].topRight, newLeft, newRight);
          b = new Square(newLeft, newRight, squares[pos].bottomLeft, squares[pos].bottomRight);
        } else if (vertical) {
          Vector2 newTop = new Vector2(initialPos.x, squares[pos].topLeft.y);
          Vector2 newBottom = new Vector2(initialPos.x, squares[pos].bottomLeft.y);
          a = new Square(newTop, squares[pos].topRight, newBottom, squares[pos].bottomRight);
          b = new Square(squares[pos].topLeft, newTop, squares[pos].bottomLeft, newBottom);
        }
        if (horizontal || vertical) {
          squares.RemoveAt(pos);
          squares.Add(a);
          squares.Add(b);
        }
      }
    }
  }

  void Draw(Vector3 destination) {
    currentLine = Instantiate(linePrefab, initialPos, Quaternion.identity);
    lineDrawer = currentLine.GetComponent<LineDrawer>();
    lineDrawer.Draw(initialPos, destination);
  }
}