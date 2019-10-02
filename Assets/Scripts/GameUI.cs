using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameUI : MonoBehaviour {
  [SerializeField]
  Button exit, mute, unmute;

  [SerializeField]
  Text levelText;

  void Start() {
    exit.onClick.AddListener(() => {
      SceneManager.LoadScene("Home");
    });
    mute.onClick.AddListener(() => {
      mute.gameObject.SetActive(false);
      unmute.gameObject.SetActive(true);
    });
    unmute.onClick.AddListener(() => {
      unmute.gameObject.SetActive(false);
      mute.gameObject.SetActive(true);
    });
    
    int level = PlayerPrefs.GetInt("level", 1);
    levelText.text = $"LEVEL {level}";
  }
}