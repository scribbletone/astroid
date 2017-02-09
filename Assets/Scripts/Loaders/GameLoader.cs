using System.Collections;
using UnityEngine;

public class GameLoader : MonoBehaviour {

  public GameObject gameManager;

  void Awake () {
    if (GameManager.instance == null) {
      Instantiate(gameManager);
    }
  }
}