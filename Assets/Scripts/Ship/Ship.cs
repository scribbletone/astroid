using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CreativeSpore.SuperTilemapEditor;

public partial class Ship : MonoBehaviour {

  public float vSpeed = 5f;
  public float hSpeed = 8f;
  public float torque = 4f;
  public float vMaxVelocity = 10f;
  public float hMaxVelocity = 1f;
  public float maxAngularVelocity = 20f;
  public float initFuel = 10f;
  public float initMaxFuel = 10f;
  public GameObject refuelingSprite;
  public bool alive = true;

  private Rigidbody2D rigidShip;
  private float verticalVelocity;
  private float horizontalVelocity;
  private float prevVerticalVelocity = 0;
  private float prevHorizontalVelocity = 0;
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
    rigidShip.constraints = RigidbodyConstraints2D.FreezeRotation;
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
      setVelocities();
      
      moveHorizontal();
      moveVertical();

      if (verticalVelocity > 0){
        Animate("thrustingForward");
        AdjustFuel(-1f);
      } else if (horizontalVelocity < 0){
        Animate("idle");
      } else if (horizontalVelocity > 0){
        Animate("idle");
      } else {
        Animate("idle");
      } 
    }
  }

  void setVelocities(){
    setVerticalVelocity();
    setHorizontalVelocity();
  }

  void setVerticalVelocity() {
    bool active = Control.IsPressed("thrust");
    // bool underSpeedLimit = rigidShip.velocity.y < vMaxVelocity;
    float force = 0f;

    if (active && (game.fuel > 0)) {
      force = 1f;
    }
    force *= vSpeed;

    verticalVelocity = force;
  }

  void setHorizontalVelocity() {
    float force = Control.HorizontalVal();
    force = dampenForce(force, prevHorizontalVelocity);
    prevHorizontalVelocity = force;
    horizontalVelocity = force * hSpeed;
  }

  float dampenForce(float force, float prevForce) {
    if (Mathf.Abs(force) > 0) {
      return force;
    } else if (Mathf.Abs(prevForce) > .1f) {
      return prevForce * 0.95f;
    } else {
      return 0;
    }
  }

  void moveVertical(){
    rigidShip.AddForce(transform.up * verticalVelocity);
    limitVerticalSpeed();
  }

  void moveHorizontal(){
    Vector2 velocity = rigidShip.velocity;
    velocity.x = horizontalVelocity;
    rigidShip.velocity = velocity;
  }

  void limitVerticalSpeed(){
    Vector3 velocity = rigidShip.velocity;
    if (velocity.y > vMaxVelocity) {
      velocity.y = vMaxVelocity;
      rigidShip.velocity = velocity;
    } else if (velocity.y < (-9f)) {
      velocity.y = -9f;
      rigidShip.velocity = velocity;
    }
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
      // AdjustHull(damage);
    }
    
  }

}
