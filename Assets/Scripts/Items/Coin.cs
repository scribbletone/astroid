using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

  public string color = "red";

  private Animator animator;

  void Start(){
    animator = GetComponent<Animator>();
    SetAnimatorColor();
  }

	public void Reset(){
    gameObject.SetActive(true);
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
}
