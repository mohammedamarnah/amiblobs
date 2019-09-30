using System;
using System.Collections.Generic;
using UnityEngine;

public class Geometry : MonoBehaviour {
  public static bool IsClosedPolygon(List<Point> point) {
    return false;
  }

  public static bool PointInConvexPolygon(List<Point> points, Point p) {
    return false;
  }
  
  public static List<Point> ConstructPolygon(Vector2[] colliderPoints) {
    List<Point> tempPoints = Point.VecToPoint(colliderPoints);
    List<Point> points = new List<Point>();
    for (int i = 0; i < tempPoints.Count; i++) {
      points.Add(tempPoints[i + 1] - tempPoints[0]);
    }

    return points;
  }

  public static bool PointInTriangle(Point a, Point b, Point c, Point p) {
    float s1 = Math.Abs(a.cross(b, c));
    float s2 = Math.Abs(p.cross(a, b)) + Math.Abs(p.cross(b, c)) + Math.Abs(p.cross(c, a));
    
    return s1 == s2;
  }

  public static bool LexComp(Point l, Point r) {
    return l.x < r.x || (l.x == r.x && l.y < r.y);
  }

  public static int sign(float val) {
    return val > 0 ? 1 : (val == 0 ? 0 : -1);
  }
}
