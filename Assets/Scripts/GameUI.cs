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
    mute.onClick.AddListener(() => {
      this.GetComponent<AudioSource>().volume = 0;
      mute.gameObject.SetActive(false);
      unmute.gameObject.SetActive(true);
    });
    unmute.onClick.AddListener(() => {
      this.GetComponent<AudioSource>().volume = 1;
      unmute.gameObject.SetActive(false);
      mute.gameObject.SetActive(true);
    });
    
    int level = PlayerPrefs.GetInt("level", 1);
    levelText.text = $"LEVEL: {level}";
  }

  public void GoHome() {
    SceneManager.LoadScene("Home");
  }
}