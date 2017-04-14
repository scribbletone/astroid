using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour {
  public static GameManager instance = null;
  
  public int level = 1;
  
  public GameObject focalObject;

  public bool clockRunning = true;
  public float clock = 0f;
  public float fuel = 10f;
  public float maxFuel = 10f;
  public float hull = 50f;
  public float maxHull = 50f;
  public Vector3 shipSpawnPoint;
  public List<GameObject> coins = new List<GameObject>();
  public bool shipRefueling = false;

  public delegate void CoinAddedEvent(GameObject coin);
  public static event CoinAddedEvent coinAddedEvent;

  public delegate void ResetSceneEvent();
  public static event ResetSceneEvent resetSceneEvent;

  public delegate void MajorDamageTakenEvent();
  public static event MajorDamageTakenEvent majorDamageTakenEvent;

  void Awake(){
    if (instance == null){
      instance = this;
    } else if (instance != this) {
      Destroy(gameObject);
    }
    DontDestroyOnLoad(gameObject);
  }

  public void ResetScene(float waitTime){
    StartCoroutine(ResetSceneTimer(waitTime));
  }

  IEnumerator ResetSceneTimer(float waitTime){
    yield return new WaitForSeconds(waitTime);

    if (resetSceneEvent != null) {
      resetSceneEvent();
    }
  }

  public void ResetAttributes(){
    fuel = maxFuel;
    clock = 0f;
    coins = new List<GameObject>();
    hull = maxHull;
  }

  void Update(){
    UpdateClock();
  }

  void UpdateClock(){
    if (clockRunning) {
      clock += Time.deltaTime;
    }
  }

  public void AddCoin(GameObject coin){
    coins.Add(coin);
    if (coinAddedEvent != null) {
      coinAddedEvent(coin);  
    }
    coin.SetActive(false);
  }

  public void HandleTakeMajorDamage(){
     if (majorDamageTakenEvent != null) {
      majorDamageTakenEvent();
    }
  }

  public Vector3 FocalPoint(){
    if (focalObject){
      return focalObject.transform.position;
    } else {
      return new Vector3(0,0,0);
    }
  }
}
