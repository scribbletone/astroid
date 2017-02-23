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
	
	void Update () {
    bool shouldAnimate = shipTouching && game.shipRefueling;
    animator.SetBool("refueling", shouldAnimate);
	}
}
