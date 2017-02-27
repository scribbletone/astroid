using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour {
  public string color = "red";

  private Animator animator;

	void Start () {
    animator = GetComponent<Animator>();
    GameManager.coinAddedEvent += HandleCoinAdded;
    GameManager.resetSceneEvent += Reset;
    Reset();
	}

  void OnDestroy() {
     GameManager.coinAddedEvent -= HandleCoinAdded;
     GameManager.resetSceneEvent -= Reset;
  }

  public void Reset () {
    SetAnimatorColor();
    gameObject.SetActive(true);
    SetColliderStatus(true);
    animator.SetBool("active", true);
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
      Deactivate();
    }
  }

  void Deactivate(){
    SetColliderStatus(false);
    // animator.SetBool("active", false);
  }

}
