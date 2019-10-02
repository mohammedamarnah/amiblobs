using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelGenerator : MonoBehaviour {
  public static float speed = 0.1f;

  [SerializeField]
  GameObject amebaPrefab;

  public static List<GameObject> generatedAmebas = new List<GameObject>();
  public static int numSpawned = (int)1e9;

  void Start() {
    Vector3 screenBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    int level = PlayerPrefs.GetInt("level", 1);
    numSpawned = level;
    for (int i = 0; i < 3 * level; i++) {
      GameObject ameba = Instantiate(amebaPrefab, Vector3.zero, Quaternion.identity);
      ameba.transform.SetParent(this.transform.parent);
      float newX = Helpers.GenerateRandom(-screenBoundaries.x + 0.7f, screenBoundaries.x - 0.7f);
      float newY = Helpers.GenerateRandom(-screenBoundaries.y + 0.7f, screenBoundaries.y - 0.7f);
      ameba.transform.position = new Vector3(newX, newY, 0);
      ameba.transform.localScale = new Vector3(0f, 0f, 0f);
      ameba.transform.DOScale(new Vector3(0.35f, 0.3f, 0.0f), 0.2f);
      generatedAmebas.Add(ameba);
    }
  }

  void OnDestroy() {
    generatedAmebas.Clear();
    numSpawned = (int)1e9;
  }

  void Update() {
    if (numSpawned == 0) {
      int currentLevel = PlayerPrefs.GetInt("level", 1);
      PlayerPrefs.SetInt("level", currentLevel + 1);
      SceneManager.LoadScene("InGame");
    }
    if (generatedAmebas.Count < 1) return;
    foreach (var ameba in generatedAmebas) {
      for (int i = 0; i < GameController.squares.Count; i++) {
        if (ameba != null && GameController.squares[i].Contains(ameba.transform.position)) {
          Square sq = GameController.squares[i];
          float newX = Helpers.GenerateRandom(sq.topLeft.x + 10, sq.topRight.x - 10);
          float newY = Helpers.GenerateRandom(sq.topLeft.y + 10, sq.bottomLeft.y - 10);
          Vector3 pos = ameba.transform.position;
          Vector3 newPos = new Vector3(pos.x - newX, pos.y - newY, ameba.transform.position.z);
          speed = LineDrawer.drawingLock ? 1.5f : 0.3f;
          ameba.transform.position = Vector3.MoveTowards(pos, newPos, speed * Time.deltaTime);
        }
      }
    }
    if (!GameController.firstLock && !LineDrawer.drawingLock) {
      foreach (var sq in GameController.squares) {
        var amebas = generatedAmebas.Where(am => sq.Contains(am.transform.position));
        if (amebas != null && amebas.Count() == 1 && amebas.First() != null) {
          AmebaMovement amv = amebas.First().GetComponent<AmebaMovement>();
          if (!amv.isDying) {
            amv.isDying = true;
            int idx = LevelGenerator.generatedAmebas.IndexOf(amebas.First());
            amebas.First().transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => {
              DestroyImmediate(amebas.First());
              LevelGenerator.generatedAmebas.RemoveAt(idx);
              numSpawned--;
            });
          }
        }
      }
    }
  }
}
