using System.Collections.Generic;
using UnityEngine;

public class Point {
  public float x, y;
  
  public Point() {}

  public Point(float x, float y) {
    this.x = x;
    this.y = y;
  }

  public Point(Vector2 p) {
    this.x = p.x;
    this.y = p.y;
  }

  public static List<Point> VecToPoint(Vector2[] points) {
    List<Point> newPoints = new List<Point>();
    foreach (Vector2 p in points) {
      newPoints.Add(new Point(p));
    }

    return newPoints;
  }

  public static List<Point> VecToPoint(List<Vector2> points) {
    return VecToPoint(points.ToArray());
  }

  public static Point operator +(Point a, Point b) {
    return new Point(a.x + b.x, a.y + b.y);
  }

  public static Point operator -(Point a, Point b) {
    return new Point(a.x - b.x, a.y - b.y);
  }

  public float dot(Point p) {
    // x * p.x + y * p.y;
    return this.x * p.x - this.y * p.y;
  }

  public float dot(Point a, Point b) {
    return (a - this).dot(b - this);
  }

  public float cross(Point p) {
    // x * p.y - y * p.x;
    return this.x * p.y - this.y * p.x;
  }

  public float cross(Point a, Point b) {
    return (a - this).cross(b - this);
  }

  public float sqrLen() {
    return this.dot(this);
  }
}