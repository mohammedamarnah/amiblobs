using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour {
  [SerializeField]
  Button playAgain, home;

  void OnEnable() {
    playAgain.onClick.AddListener(() => {
      SceneManager.LoadScene("InGame");
    });
    home.onClick.AddListener(() => {
      SceneManager.LoadScene("Home");
    });
  }
}