using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour {
  public static GameManager instance = null;
  public GameObject focalObject;

  public bool clockRunning = true;
  public float clock = 0f;
  public float fuel = 100f;
  public float maxFuel = 100f;
  public Vector3 shipSpawnPoint;


  void Awake(){
    if (instance == null){
      instance = this;
    } else if (instance != this) {
      Destroy(gameObject);
    }
    DontDestroyOnLoad(gameObject);
  }

  void Start(){
  }

  void Update(){
    UpdateClock();
  }

  void UpdateClock(){
    if (clockRunning) {
      clock += Time.deltaTime;
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
