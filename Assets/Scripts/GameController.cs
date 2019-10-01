using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
  [SerializeField]
  public GameObject linePrefab;
  
  public static GameObject currentLine;
  private List<GameObject> allLines = new List<GameObject>();

  private List<Vector2> fingerPositions = new List<Vector2>();
  private Vector2 startingPoint;
  private LineRenderer lineRenderer;
  private EdgeCollider2D edgeCollider;
  private Polygon polygon;

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
    startingPoint = fingerPositions[0];
    edgeCollider.points = fingerPositions.ToArray();
    polygon.Points = Point.VecToPoint(edgeCollider.points);
    AmebaMovement.line = edgeCollider;
    allLines.Add(currentLine);
    StartCoroutine(DestroyAfter(2.5f));
  }

  void UpdateLine(Vector2 fingerPos) {
    if (currentLine != null) {
      CheckForClosed(fingerPos);
      fingerPositions.Add(fingerPos);
      lineRenderer.positionCount++;
      lineRenderer.SetPosition(lineRenderer.positionCount - 1, fingerPos);
      edgeCollider.points = fingerPositions.ToArray();
      polygon.Points = Point.VecToPoint(edgeCollider.points);
      AmebaMovement.line = edgeCollider;
      // if (Math.Abs(startingPoint.x - fingerPos.x) < 1f
      // && Math.Abs(startingPoint.y - fingerPos.y) < 1f) {
    }
  }

  void CheckForClosed(Vector2 fingerPos) {
    foreach (var p in fingerPositions) {
      if (Math.Abs(p.x - fingerPos.x) < 0.5f && Math.Abs(p.y - fingerPos.y) < 0.5f) {
        fingerPositions.Add(fingerPos);
        polygon.Points = Point.VecToPoint(fingerPositions);
        polygon.CheckIfClosed();
        fingerPositions.RemoveAt(fingerPositions.Count - 1);
        Debug.Log("is closed? " + polygon.isClosed);
        break;
      }
    }
  }

  IEnumerator DestroyAfter(float seconds) {
    yield return new WaitForSeconds(seconds);
    // foreach (var line in allLines) {
    //   if (line != null) {
    //     Polygon p = line.GetComponent<Polygon>();
    //     if (!p.isClosed) {
    //       Destroy(p);
    //     }
    //   }
    // }
    if (currentLine != null && !polygon.isClosed) {
      Destroy(currentLine);
    }
    currentLine = null;
    // allLines.Clear();
  }
}