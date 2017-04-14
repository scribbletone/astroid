using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CreativeSpore.SuperTilemapEditor;

public partial class Ship : MonoBehaviour {

  public float speed = 0.3f;
  public float torque = 4f;
  public float maxVelocity = 1.5f;
  public float maxAngularVelocity = 20f;
  public float initFuel = 10f;
  public float initMaxFuel = 10f;
  public GameObject refuelingSprite;
  public bool alive = true;

  private float speedBoost = 2f;
  private Rigidbody2D rigidShip;
  private float verticalForce;
  private float horizontalForce;
  private Vector3 directionVector;
  private Animator animator;
  private Animator refuelingAnimator;
  private GameManager game;

  void OnDestroy() {     GameManager.resetSceneEvent -= Reset;
  }

  void Start () {
    animator = GetComponent<Animator>();
    refuelingAnimator = refuelingSprite.GetComponent<Animator>();
    rigidShip = GetComponent<Rigidbody2D>();
    game = GameManager.instance;
    
    InitShipAttributes();
    GameManager.resetSceneEvent += Reset;
  }

  public void Reset() {
    alive = true;
    Halt();
    transform.eulerAngles = new Vector3(0, 0, 0);
    rigidShip.constraints = RigidbodyConstraints2D.None;
    transform.position = game.shipSpawnPoint;
    Animate("idle");
  }

  void Halt(){
    rigidShip.velocity = Vector3.zero;
    rigidShip.angularVelocity = 0;
  }

  void Freeze(){
    Halt();
    rigidShip.constraints = RigidbodyConstraints2D.FreezeAll;
  }

  void InitShipAttributes() {
    game.maxFuel = initMaxFuel;
    game.fuel = initFuel;
  }

  void FixedUpdate() { 
    MoveShipFromInput();
  }

  void MoveShipFromInput(){
    if (alive) {
      verticalForce = ForwardThrust();
      horizontalForce = Control.HorizontalVal();

      if (game.fuel <= 0) {
        verticalForce = 0;
        horizontalForce = horizontalForce * 0.1f;
      }

      rigidShip.AddForce(transform.up * verticalForce * speed * speedBoost);
      rigidShip.AddTorque(torque * horizontalForce * -1f);

      rigidShip.velocity = Vector3.ClampMagnitude(rigidShip.velocity, maxVelocity * speedBoost);
      LimitRotation();

      if (verticalForce > 0){
        Animate("thrustingForward");
        AdjustFuel(-1f);
      } else if (horizontalForce < 0){
        Animate("rotatingLeft");
      } else if (horizontalForce > 0){
        Animate("rotatingRight");
      } else {
        Animate("idle");
      } 
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

  public void AdjustFuel(float inAmount){
    float newFuelLevel = (inAmount / 20) + game.fuel;

    if (newFuelLevel > game.maxFuel) {
      newFuelLevel = game.maxFuel;
    } else if (newFuelLevel < 0){
      newFuelLevel = 0;
    }

    if (game.fuel < game.maxFuel && inAmount > 0) {
      game.shipRefueling = true;
    } else {
      game.shipRefueling = false;
    }

    // game.fuel = newFuelLevel;
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
    } else if (inAmount < 0) {
      AnimateDamage();
    }
  }

  public void handleExplode(){
    alive = false;
    Freeze();
    game.shipRefueling = false;
    Animate("exploding");
    game.ResetScene(2f);
  }

  void LimitRotation(){

    if (rigidShip.angularVelocity > maxAngularVelocity){
      rigidShip.angularVelocity = maxAngularVelocity;
    } else if (rigidShip.angularVelocity < (maxAngularVelocity * -1)){
      rigidShip.angularVelocity = maxAngularVelocity * -1;
    }
  }

  void OnCollisionEnter2D(Collision2D other) {
    Tilemap tilemap = other.gameObject.GetComponentInParent<Tilemap>();
    if (tilemap != null){
      float damage = other.relativeVelocity.magnitude;
      damage = damage * damage * -1f;
      if (damage > -1)
        damage = 0;
      AdjustHull(damage);
    }
    
  }

}
