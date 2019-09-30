using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmebaMovement : MonoBehaviour {
  public static float speed = 0.1f;

  void Update() {
    // Debug.Log(speed);
    float newX = UnityEngine.Random.Range(-2.5f, 2.5f);
    float newY = UnityEngine.Random.Range(-4.5f, 4.5f);
    Vector3 newPos = new Vector3(newX, newY, transform.position.z);
    Vector3 pos = transform.position;
    if (speed == 0.1f) {
      transform.Translate(newPos * Time.deltaTime * speed, Space.Self);
    } else {
      transform.position = Vector3.Lerp(pos, newPos, speed * Time.deltaTime);
    }
  }
}
