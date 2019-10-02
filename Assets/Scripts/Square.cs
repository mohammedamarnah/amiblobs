using UnityEngine;

public class Square {
  public Vector2 topLeft, topRight, bottomLeft, bottomRight;

  public Square() {}

  public Square(Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight) {
    this.topLeft = topLeft;
    this.topRight = topRight;
    this.bottomLeft = bottomLeft;
    this.bottomRight = bottomRight;
  }
  
  public bool Contains(Vector2 point) {
    return point.x > topLeft.x && point.x < topRight.x &&
           point.y > bottomLeft.y && point.y < topLeft.y;
  }

  public bool Contains(Vector3 point) {
    return Contains(new Vector2(point.x, point.y));
  }

  public void Print() {
    Debug.Log(topLeft + " " + topRight + " " + bottomLeft + " " + bottomRight);
  }
}