using System.Collections.Generic;
using UnityEngine;

public class Polygon : MonoBehaviour {
  public bool isClosed = false;
  private float AREA_THRESHOLD = 1f;
  private List<Point> polyPoints;
  public List<Point> Points {
    get { return polyPoints; }
    set { polyPoints = value; }
  }

  public void CheckIfClosed() {
    float area = Geometry.PolygonArea(this.polyPoints);
    Debug.Log("the area: " + area);
    Debug.Log("threshold: " + AREA_THRESHOLD);
    if (area > AREA_THRESHOLD) {
      this.isClosed = true;
    }
  }
}
