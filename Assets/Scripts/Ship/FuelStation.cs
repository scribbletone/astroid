using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelStation : MonoBehaviour {
  public bool shipTouching = false;
  private GameManager game;
  private Animator animator;

	void Start () {
    game = GameManager.instance;	
    animator = GetComponent<Animator>();
	}

  void OnTriggerExit2D(Collider2D other) {
    if (other.gameObject.tag == "Player"){
      animator.SetBool("refueling", false);
    }
  }

  void OnTriggerStay2D(Collider2D other) {
    if (other.gameObject.tag == "Player"){
      Ship ship = other.gameObject.GetComponent<Ship>();

      if (ship.alive) {
        ship.AdjustFuel(1f);
      }

      if (game.shipRefueling) {
        animator.SetBool("refueling", true);
      } else {
        animator.SetBool("refueling", false);
      }
    }
  }
}
