using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour {

  public float speed = 0.3f;
  public float torque = 4f;
  public float maxVelocity = 1.5f;
  public float maxAngularVelocity = 20f;
  public float fuel = 100f;
  public float maxFuel = 100f;

  private float speedBoost = 2f;
  private Rigidbody2D rigidShip;
  private float verticalForce;
  private float horizontalForce;
  private Vector3 directionVector;
  private Animator animator;
  private GameManager game;

  void Start () {
    animator = GetComponent<Animator>();
    rigidShip = GetComponent<Rigidbody2D> ();
    game = GameManager.instance;
    game.maxFuel = maxFuel;
  }

  void FixedUpdate() { 
    verticalForce = Input.GetAxis ("Vertical");
    horizontalForce = Input.GetAxis ("Horizontal");

    if (fuel > 0) {
      rigidShip.AddForce(transform.up * verticalForce * speed * speedBoost);
      rigidShip.AddTorque(torque * horizontalForce * -1f);

      rigidShip.velocity = Vector3.ClampMagnitude(rigidShip.velocity, maxVelocity * speedBoost);
      limitRotation();

      if (verticalForce > 0){
        animator.SetTrigger("shipThrusting");
        adjustFuel(-1f);
      } else if (horizontalForce < 0){
        animator.SetTrigger("shipRotatingLeft");
        adjustFuel(-0.25f);
      } else if (horizontalForce > 0){
        animator.SetTrigger("shipRotatingRight");
        adjustFuel(-0.25f);
      } else {
        animator.SetTrigger("shipStopThrusting");
      } 

    } else {
      animator.SetTrigger("shipStopThrusting");
    }
  }

  void adjustFuel(float amount){
    fuel += amount / 20;
    game.fuel = fuel;
  }

  void limitRotation(){
    if (rigidShip.angularVelocity > maxAngularVelocity){
      rigidShip.angularVelocity = maxAngularVelocity;
    } else if (rigidShip.angularVelocity < (maxAngularVelocity * -1)){
      rigidShip.angularVelocity = maxAngularVelocity * -1;
    }
  }

  void OnCollisionEnter2D(Collision2D coll) {
    if (coll.gameObject.tag == "wall"){
      print("wall");
    }
  }
}
