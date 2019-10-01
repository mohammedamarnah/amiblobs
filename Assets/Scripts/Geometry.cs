using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Geometry : MonoBehaviour {
  public static float PolygonArea(List<Point> polygon) {
    float res = 0.0f;
    for (int i = 0; i < polygon.Count; i++) {
      Point p;
      if (i == 0) p = polygon.Last();
      else p = polygon[i - 1];
      Point q = polygon[i];
      res += (p.x - q.x) * (p.y + q.y);
    }

    return Math.Abs(res) / 2;
  }

  public static bool PointInConvexPolygon(List<Point> polygon, Point point) {
    if (polygon.First().cross(point) != 0 && sign(polygon.First().cross(point)) != sign(polygon.First().cross(polygon.Last()))) {
      return false;
    }
    if (polygon.Last().cross(point) != 0 && sign(polygon.Last().cross(point)) != sign(polygon.Last().cross(polygon.First()))) {
      return false;
    }
    if (polygon.First().cross(point) == 0) {
      return polygon.First().sqrLen() >= point.sqrLen();
    }

    int l = 0, r = polygon.Count - 1;
    while (r - l > 1) {
      int mid = (l + r) >> 1;
      if (polygon[mid].cross(point) >= 0) {
        l = mid;
      } else {
        r = mid;
      }
    }
    int pos = l;

    return PointInTriangle(polygon[pos], polygon[pos + 1], new Point(0f, 0f), point);
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
