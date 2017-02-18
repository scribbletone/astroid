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
    rigidShip = GetComponent<Rigidbody2D>();
    game = GameManager.instance;
    
    InitShipAttributes();
  }

  public void Reset() {
    rigidShip.velocity = Vector3.zero;
    rigidShip.angularVelocity = 0;
    transform.eulerAngles = new Vector3(0, 0, 0);
    transform.position = game.shipSpawnPoint;
  }

  void InitShipAttributes() {
    game.maxFuel = initMaxFuel;
    game.fuel = initFuel;
  }

  void FixedUpdate() { 
    verticalForce = ForwardThrust();
    horizontalForce = Control.HorizontalVal();

    if (game.fuel > 0) {
      rigidShip.AddForce(transform.up * verticalForce * speed * speedBoost);
      rigidShip.AddTorque(torque * horizontalForce * -1f);

      rigidShip.velocity = Vector3.ClampMagnitude(rigidShip.velocity, maxVelocity * speedBoost);
      LimitRotation();

      if (verticalForce > 0){
        animator.SetTrigger("shipThrusting");
        AdjustFuel(-1f);
      } else if (horizontalForce < 0){
        animator.SetTrigger("shipRotatingLeft");
        AdjustFuel(-0.05f);
      } else if (horizontalForce > 0){
        animator.SetTrigger("shipRotatingRight");
        AdjustFuel(-0.05f);
      } else {
        animator.SetTrigger("shipStopThrusting");
      } 

    } else {
      animator.SetTrigger("shipStopThrusting");
    }
  }

  float ForwardThrust() {
    bool active = Control.IsPressed("thrust");
    float force = 0f;

    if (active) {
      force = 1f;
    }

    return force;
  }

  void AdjustFuel(float inAmount){
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

  public void AdjustHull(float inAmount){
    float newHullLevel = game.hull + inAmount;

    if (newHullLevel > game.maxHull) {
      newHullLevel = game.maxHull;
    } else if (newHullLevel < 0) {
      newHullLevel = 0;
    }

    game.hull = newHullLevel;

    if (game.hull <= 0){
      handleExplode();
    }
  }

  public void handleExplode(){
    print("kapow");
    game.RestartScene();
  }


  void LimitRotation(){

    if (rigidShip.angularVelocity > maxAngularVelocity){
      rigidShip.angularVelocity = maxAngularVelocity;
    } else if (rigidShip.angularVelocity < (maxAngularVelocity * -1)){
      rigidShip.angularVelocity = maxAngularVelocity * -1;
    }
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.tag == "Coin"){
      other.gameObject.SetActive(false);
      game.coins.Add(other.gameObject);
    }
  }

  void OnTriggerStay2D(Collider2D other) {
    if (other.gameObject.tag == "FuelStation"){
      AdjustFuel(1f);
    }
  }

  void OnCollisionEnter2D(Collision2D other) {

    if (other.gameObject.tag == "Wall"){
      float damage = other.relativeVelocity.magnitude;
      damage = damage * damage * -1f;
      if (damage > -1)
        damage = 0;
      AdjustHull(damage);
    }
    
  }

}
