using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour {
  public string color = "red";
  public float offsetTime = 0f;
  public float activeTime = 1f;
  public float inactiveTime = 1f;

  private Animator animator;
  private Renderer renderer;
  private IEnumerator coroutine;
  private int flickerFrame = 1;
  private bool visible = true;
  private bool firstActivation = true;

	void Start () {
    animator = GetComponent<Animator>();
    renderer = GetComponent<Renderer>();
    GameManager.coinAddedEvent += HandleCoinAdded;
    GameManager.resetSceneEvent += Reset;
    Reset();
    coroutine = Flicker();
    StartCoroutine(coroutine);
	}

  void OnDestroy() {
     GameManager.coinAddedEvent -= HandleCoinAdded;
     GameManager.resetSceneEvent -= Reset;
  }

  public void Reset () {
    SetAnimatorColor();
    gameObject.SetActive(true);
    firstActivation = true;
    Reactivate();
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

  private IEnumerator Flicker() {
    print("flicker");
    while (true) {
      float waitTime = inactiveTime;

      if (visible)
        waitTime = activeTime;

      if (firstActivation) {
        waitTime += offsetTime;
        firstActivation = false;
      }

      yield return new WaitForSeconds(waitTime);
      Toggle();
    }
  }

  void Toggle(){
    if (visible) {
      Deactivate();
    } else {
      Reactivate();
    }
  }

  void Reactivate (){
    SetColliderStatus(true);
    renderer.enabled = true;
    visible = true;
  }

  void Deactivate (){
    SetColliderStatus(false);
    renderer.enabled = false;
    visible = false;
    // animator.SetBool("active", false);
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

  void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.tag == "Player") {
      Ship ship = other.gameObject.GetComponent<Ship>();
      ship.AdjustHull(-50f);
    }
  }

}
