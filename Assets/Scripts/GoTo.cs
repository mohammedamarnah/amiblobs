using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GoTo : MonoBehaviour {
  [SerializeField]
  string scene;

  void Start() {
    this.GetComponent<Button>().onClick.AddListener(() => {
        SceneManager.LoadScene(scene);
    });
  }
}