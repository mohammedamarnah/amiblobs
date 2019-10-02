using UnityEngine;

public class AmebaMovement : MonoBehaviour {
  public static float speed = 0.1f;
  public static EdgeCollider2D line = new EdgeCollider2D();
  bool stop = false;  

  void Update() {
    if (!stop) {
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
}