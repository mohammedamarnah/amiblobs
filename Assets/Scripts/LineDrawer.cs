using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour {
  public static bool drawingLock = false;
  bool drawing = false;
  Vector3 origin, destination;
  LineRenderer lineRenderer;
  EdgeCollider2D edgeCollider;
  float distance, counter = 0, drawingSpeed = 10f;

  public void Draw(Vector3 origin, Vector3 destination, int dir) {
    this.origin = origin;
    this.destination = destination;
    lineRenderer = this.GetComponent<LineRenderer>();
    edgeCollider = this.GetComponent<EdgeCollider2D>();
    lineRenderer.SetPosition(0, origin);
    distance = Vector3.Distance(origin, destination);
    List<Vector2> points = new List<Vector2>();
    points.Add(Vector3.zero);
    if (dir > 0) {
      points.Add(new Vector3(0, destination.y, 0));
    } else {
      points.Add(new Vector3(destination.x, 0, 0));
    }
    edgeCollider.points = points.ToArray();
    drawing = true;
  }

  void Update() {
    if (drawing) {
      float x = Mathf.Lerp(0, distance, counter + .1f / drawingSpeed);
      Vector3 a = origin;
      Vector3 b = destination;
      Vector3 pointAlongTheLine = x * Vector3.Normalize(b - a) + a;
      Vector3 diff = destination - pointAlongTheLine;
      if (Math.Abs(diff.magnitude) >= 1e-5) {
        lineRenderer.SetPosition(1, pointAlongTheLine);
        counter += .1f / drawingSpeed;
      } else {
        drawing = false;
      }
      drawingLock = true;
    } else {
      drawingLock = false;
      GameController.firstLock = false;
    }
  }
}
