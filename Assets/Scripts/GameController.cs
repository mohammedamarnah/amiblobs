using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
  [SerializeField]
  public GameObject linePrefab;
  
  [NonSerialized]
  public static GameObject currentLine;

  [NonSerialized]
  public List<Vector2> fingerPositions = new List<Vector2>();

  [NonSerialized]
  public LineRenderer lineRenderer;
  [NonSerialized]
  public EdgeCollider2D edgeCollider;
  [NonSerialized]
  public Polygon polygon;

  void Update() {
    if(Input.GetMouseButtonDown(0)) {
      AmebaMovement.speed = 0.3f;
      CreateLine();
    }
    if(Input.GetMouseButton(0) && currentLine != null) {
      Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      if(Vector2.Distance(tempFingerPos, fingerPositions.Last()) > 0.1f) {
        UpdateLine(tempFingerPos);
      }
    }
    if (Input.GetMouseButtonUp(0)) {
      AmebaMovement.speed = 0.1f;
    }
  }

  void CreateLine() {
    currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
    lineRenderer = currentLine.GetComponent<LineRenderer>();
    edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
    polygon = currentLine.GetComponent<Polygon>();
    fingerPositions.Clear();
    fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    lineRenderer.SetPosition(0, fingerPositions[0]);
    lineRenderer.SetPosition(1, fingerPositions[1]);
    edgeCollider.points = fingerPositions.ToArray();
    AmebaMovement.line = edgeCollider;
    StartCoroutine(DestroyAfter(1f));
  }

  void UpdateLine(Vector2 fingerPos) {
    if (currentLine != null) {
      fingerPositions.Add(fingerPos);
      lineRenderer.positionCount++;
      lineRenderer.SetPosition(lineRenderer.positionCount - 1, fingerPos);
      edgeCollider.points = fingerPositions.ToArray();
      AmebaMovement.line = edgeCollider;
    }
  }

  IEnumerator DestroyAfter(float seconds) {
    yield return new WaitForSeconds(seconds);
    if (currentLine != null && !polygon.isClosed) {
      Destroy(currentLine);
      currentLine = null;
    }
  }
}