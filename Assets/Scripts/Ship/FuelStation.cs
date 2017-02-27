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

  void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.tag == "Player"){
      animator.SetBool("refueling", true);
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    if (other.gameObject.tag == "Player"){
      if (other.gameObject.tag == "Player"){
        animator.SetBool("refueling", false);
      }
    }
  }

  void OnTriggerStay2D(Collider2D other) {
    if (other.gameObject.tag == "Player"){
      Ship ship = other.gameObject.GetComponent<Ship>();

      if (ship.alive) {
        ship.AdjustFuel(1f);
      }
    }
  }
}
