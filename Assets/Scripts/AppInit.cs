using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AppInit : MonoBehaviour {
  [SerializeField]
  Slider slider;
  bool finished = false;

  void Update() {
    if (finished) return;
    slider.value += 0.01f;
    if (slider.value >= 1) {
      SceneManager.LoadScene("Home");
      finished = true;
    }
  }
}