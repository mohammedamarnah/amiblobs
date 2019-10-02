using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour {
  bool drawing = false;
  Vector2 origin, destination;
  LineRenderer lineRenderer;
  float distance, counter = 0, drawingSpeed = 10f;

  public void Draw(Vector3 origin, Vector3 destination) {
    this.origin = origin;
    this.destination = destination;
    lineRenderer = this.GetComponent<LineRenderer>();
    lineRenderer.SetPosition(0, origin);
    distance = Vector2.Distance(origin, destination);
    drawing = true;
  }

  void Update() {
    if (drawing) {
      if (counter < distance) {
        counter += .1f / drawingSpeed;
        float x = Mathf.Lerp(0, distance, counter);
        Vector3 a = origin;
        Vector3 b = destination;
        Vector3 pointAlongTheLine = x * Vector3.Normalize(b - a) + a;
        lineRenderer.SetPosition(1, pointAlongTheLine);
      }
    }
  }
}
