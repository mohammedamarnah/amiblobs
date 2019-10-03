using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelGenerator : MonoBehaviour {
  [SerializeField]
  GameObject amebaPrefab;

  [SerializeField]
  ParticleSystem ps;

  [SerializeField]
  Text countText;

  public static List<GameObject> generatedAmebas = new List<GameObject>();
  public static int numSpawned = (int)1e9;
  public bool deathLock = false;
  public bool levelUp = false;

  void Start() {
    Vector3 screenBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    int level = PlayerPrefs.GetInt("level", 1);
    numSpawned = level + 1;
    countText.text = $":{numSpawned}";
    for (int i = 0; i < numSpawned; i++) {
      float newX = Helpers.GenerateRandom(-screenBoundaries.x + 0.7f, screenBoundaries.x - 0.7f);
      float newY = Helpers.GenerateRandom(-screenBoundaries.y + 0.7f, screenBoundaries.y - 0.7f);
      GameObject ameba = Instantiate(amebaPrefab, new Vector3(newX, newY, 0), Quaternion.identity);
      ameba.transform.SetParent(this.transform.parent);
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
    if (levelUp) return;
    if (numSpawned == 0) {
      StartCoroutine(LevelUp());
      levelUp = true;
    }
    if (generatedAmebas.Count < 1) return;
    if (!GameController.firstLock && !LineDrawer.drawingLock) {
      foreach (var sq in GameController.squares) {
        var amebas = generatedAmebas.Where(am => 
                      sq.Contains(am.transform.position)
                      && !am.GetComponent<AmebaMovement>().dead);
        if (amebas != null && amebas.Count() == 1 && amebas.First() != null && !deathLock) {
          deathLock = true;
          amebas.First().transform.DOScale(Vector3.zero, 0.4f).OnComplete(() => {
            deathLock = false;
            amebas.First().GetComponent<AmebaMovement>().dead = true;
            numSpawned--;
            countText.text = $":{numSpawned}";
          });
        }
      }
    }
  }

  IEnumerator LevelUp() {
    AudioClip clip = (AudioClip)Resources.Load("WINNING_THE_LEVEL");
    this.GetComponent<AudioSource>().clip = clip;
    this.GetComponent<AudioSource>().Play();
    ps.gameObject.SetActive(true);
    yield return new WaitForSeconds(3f);
    int currentLevel = PlayerPrefs.GetInt("level", 1);
    PlayerPrefs.SetInt("level", currentLevel + 1);
    SceneManager.LoadScene("InGame");
  }
}
