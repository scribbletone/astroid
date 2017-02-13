using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour {

  private GameManager game;

	void Start () {
    game = GameManager.instance;	
	}

  void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.CompareTag("Player")) {
      game.level += 1;
      SceneManager.LoadScene("LevelTitle");
    }
  }
}
