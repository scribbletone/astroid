using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour {
  public int unlockAt = 10;
  private GameManager game;

	void Start () {
    game = GameManager.instance;	
	}
	
	void Update () {
    if (game.coins.Count >= unlockAt) {
      gameObject.SetActive(false);
    }
	}
}
