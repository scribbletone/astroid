using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour {
  public int unlockAt = 10;
  public string color = "red";

  private GameManager game;
  private Animator animator;

  void OnEnable(){
    GameManager.coinAddedEvent += HandleCoinAdded;
  }

  void OnDisable() {
     GameManager.coinAddedEvent -= HandleCoinAdded;
  }

	void Start () {
    game = GameManager.instance;
    animator = GetComponent<Animator>();
    SetAnimatorColor();
	}

  public void Reset () {
    gameObject.SetActive(true);
    SetColliderStatus(true);
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

  public void SetColliderStatus(bool status) {
    foreach(BoxCollider2D c in GetComponents<BoxCollider2D>()) {
      print(c);
      c.enabled = status;
    }
  }

  void HandleCoinAdded(GameObject coin){
    if (color == coin.GetComponent<Coin>().color) {
      SetColliderStatus(false);
    }
  }

}
