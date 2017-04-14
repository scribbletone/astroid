using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

  public string color = "red";

  private GameManager game;
  private Animator animator;

  void Start(){
    game = GameManager.instance;
    animator = GetComponent<Animator>();
    SetAnimatorColor();
    GameManager.resetSceneEvent += Reset;
  }

  void OnDestroy() {     GameManager.resetSceneEvent -= Reset;
  }

	public void Reset(){
    gameObject.SetActive(true);
    SetAnimatorColor();
  }

  void SetAnimatorColor(){
    switch (color) {
      case "green":
        animator.SetInteger("colorIndex", 1);
        break;
      case "orange":
        animator.SetInteger("colorIndex", 2);
        break;
      case "silver":
        animator.SetInteger("colorIndex", 3);
        break;
      default: // red
        animator.SetInteger("colorIndex", 0);
        break;
    }
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.tag == "Player"){
      game.AddCoin(gameObject);
    }
  }
}
