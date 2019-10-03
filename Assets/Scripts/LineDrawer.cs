using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LineDrawer : MonoBehaviour {
  public static bool drawingLock = false;
  public static int currentDrawingID;
  bool drawing = false;
  public int id; 
  Vector3 origin, destination;
  LineRenderer lineRenderer;
  EdgeCollider2D edgeCollider;
  float distance, counter = 0, drawingSpeed = 3f;
  int dir = -1, dir2 = 0;

  public void Draw(Vector3 origin, Vector3 destination, int dir, int dir2) {
    this.origin = origin;
    this.destination = destination;
    lineRenderer = this.GetComponent<LineRenderer>();
    edgeCollider = this.GetComponent<EdgeCollider2D>();
    lineRenderer.SetPosition(0, origin);
    distance = Vector3.Distance(origin, destination);
    this.dir = dir;
    this.dir2 = dir2;
    drawing = true;
  }

  void Update() {
    if (drawing) {
      currentDrawingID = id;
      float x = Mathf.Lerp(0, distance, counter + .1f / drawingSpeed);
      Vector3 a = origin;
      Vector3 b = destination;
      Vector3 pointAlongTheLine = x * Vector3.Normalize(b - a) + a;
      Vector3 diff = destination - pointAlongTheLine;
      if (Math.Abs(diff.magnitude) >= 1e-6) {
        lineRenderer.SetPosition(1, pointAlongTheLine);
        counter += .1f / drawingSpeed;
        List<Vector2> points = new List<Vector2>();
        points.Add(Vector3.zero);
        if (dir > 0) {
          points.Add(new Vector3(0, dir2*x, 0));
        } else {
          points.Add(new Vector3(dir2*x, 0, 0));
        }
        edgeCollider.points = points.ToArray();
      } else {
        currentDrawingID = -1;
        drawing = false;
      }
      drawingLock = true;
    } else {
      drawingLock = false;
      GameController.firstLock = false;
    }
  }

  void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.CompareTag("Ameba") && drawingLock && LevelGenerator.numSpawned != 0) {
      if (id != currentDrawingID) return;
      GameController.youLost = true;
      GameObject gm = GameObject.Find("Canvas");
      gm.transform.Find("PlayAgainPopup").gameObject.SetActive(true);
    }
  }
}
