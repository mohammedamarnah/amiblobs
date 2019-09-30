using System.Collections.Generic;
using UnityEngine;

public class Polygon : MonoBehaviour {
  public bool isClosed = false;
  private List<Point> polyPoints;
  public List<Point> Points {
    get { return polyPoints; }
    set { polyPoints = value; }
  }
}
