using System;
using UnityEngine;

public class AmebaMovement : MonoBehaviour {
  public bool isDying = false;
  public bool dead = false;
  public bool velocityUpdated = false;
  public bool velocityReset = true;
  public Rigidbody2D rigidBody;

  void Awake() {
    rigidBody = this.GetComponent<Rigidbody2D>();
    int a = Helpers.GenerateRandom(1, 2);
    int b = Helpers.GenerateRandom(1, 2);
    float val = 0.2f;
    rigidBody.velocity = new Vector3(a == 1 ? val : -val, b == 1 ? val : -val, 0);
  }

  void Update() {
    if (LineDrawer.drawingLock) {
      if (!velocityUpdated) {
        if (Math.Abs(rigidBody.velocity.x) != 5 || Math.Abs(rigidBody.velocity.y) != 5) {
          int a = Helpers.GenerateRandom(1, 2);
          int b = Helpers.GenerateRandom(1, 2);
          int val = 2;
          rigidBody.velocity = new Vector3(a == 1 ? val : -val, b == 1 ? val : -val);
          velocityUpdated = true;
        }
      }
    }
    //   if (!velocityReset) {
    //     int a = Helpers.GenerateRandom(1, 2);
    //     int b = Helpers.GenerateRandom(1, 2);
    //     float val = 0.4f;
    //     rigidBody.velocity = new Vector3(a == 1 ? val : -val, b == 1 ? val : -val, 0);
    //     velocityReset = true;
    //   }
    //   velocityUpdated = false;
    // }

    if (Math.Abs(rigidBody.velocity.x) <= 1e-3 || Math.Abs(rigidBody.velocity.y) <= 1e-3) {
      int a = Helpers.GenerateRandom(1, 2);
      int b = Helpers.GenerateRandom(1, 2);
      float val = LineDrawer.drawingLock ? 2f : 0.2f;
      rigidBody.velocity = new Vector3(a == 1 ? val : -val, b == 1 ? val : -val);
    }
  }

  void OnCollisionEnter2D(Collision2D other) {
    // if (other.gameObject.CompareTag("Ameba")) {
    //   int a = Helpers.GenerateRandom(1, 2);
    //   int b = Helpers.GenerateRandom(1, 2);
    //   int val = 2;
    //   rigidBody.velocity = new Vector3(a == 1 ? val : -val, b == 1 ? val : -val);
    //   velocityUpdated = true;
    //   velocityReset = false;
    // }
  }
}