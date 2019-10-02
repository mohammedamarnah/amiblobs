using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour {
  [SerializeField]
  Button playNow, about, settings;
  [SerializeField]
  GameObject[] levels = new GameObject[4];

  void Start() {
    playNow.onClick.AddListener(() => {
      SceneManager.LoadScene("InGame");
    });
    int currentLevel = PlayerPrefs.GetInt("level", 1);
    int maxLevel = currentLevel;
    while (maxLevel % 4 != 0) maxLevel++;
    int minLevel = maxLevel - 4 + 1;
    for (int i = minLevel, j = 0; i <= maxLevel; i++, j++) {
      Image img = levels[j].GetComponentInChildren<Image>();
      Text txt = levels[j].GetComponentInChildren<Text>();
      txt.text = $"Level {i}";
      Color clr, txtClr;
      if (i == currentLevel) {
        clr = new Color32(255, 255, 255, 255);
        txtClr = new Color32(102, 255, 185, 255);
      } else {
        clr = new Color32(255, 255, 255, 100);
        txtClr = new Color32(102, 255, 185, 100);
      }
      img.color = clr;
      txt.color = txtClr;
    }
  }
}