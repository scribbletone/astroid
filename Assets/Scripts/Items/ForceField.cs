using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour {
  public string color = "red";

  private Animator animator;

	void Start () {
    animator = GetComponent<Animator>();
    SetAnimatorColor();
    GameManager.coinAddedEvent += HandleCoinAdded;
    GameManager.resetSceneEvent += Reset;
	}

  void OnDestroy() {
     GameManager.coinAddedEvent -= HandleCoinAdded;
     GameManager.resetSceneEvent -= Reset;
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
      c.enabled = status;
    }
  }

  void HandleCoinAdded(GameObject coin){
    if (color == coin.GetComponent<Coin>().color) {
      SetColliderStatus(false);
    }
  }

}
