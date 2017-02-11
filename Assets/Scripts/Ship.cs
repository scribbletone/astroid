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
  public float initFuel = 10f;
  public float initMaxFuel = 10f;
  public GameObject refuelingSprite;

  private float speedBoost = 2f;
  private Rigidbody2D rigidShip;
  private float verticalForce;
  private float horizontalForce;
  private Vector3 directionVector;
  private Animator animator;
  private Animator refuelingAnimator;
  private GameManager game;


  void Start () {
    animator = GetComponent<Animator>();
    refuelingAnimator = refuelingSprite.GetComponent<Animator>();
    rigidShip = GetComponent<Rigidbody2D> ();
    game = GameManager.instance;
    
    InitShipAttributes();
  }

  void InitShipAttributes(){
    game.maxFuel = initMaxFuel;
    game.fuel = initFuel;
  }

  void FixedUpdate() { 
    verticalForce = Input.GetAxis ("Vertical");
    horizontalForce = Input.GetAxis ("Horizontal");

    if (game.fuel > 0) {
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

  void adjustFuel(float inAmount){
    float newFuelLevel = (inAmount / 20) + game.fuel;

    if (newFuelLevel > game.maxFuel) {
      newFuelLevel = game.maxFuel;
    } else if (newFuelLevel < 0){
      newFuelLevel = 0;
    }

    if (game.fuel < game.maxFuel) {
      if (inAmount > 0) {
        refuelingAnimator.SetTrigger("refueling");
      }
    }

    game.fuel = newFuelLevel;
  }

  void limitRotation(){
    if (rigidShip.angularVelocity > maxAngularVelocity){
      rigidShip.angularVelocity = maxAngularVelocity;
    } else if (rigidShip.angularVelocity < (maxAngularVelocity * -1)){
      rigidShip.angularVelocity = maxAngularVelocity * -1;
    }
  }

  void OnTriggerStay2D(Collider2D other) {
    if (other.gameObject.tag == "FuelStation"){
      adjustFuel(1f);
    }
  }

}
