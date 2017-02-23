using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour {
  public static GameManager instance = null;
  
  public int level = 1;
  public delegate void RestartSceneDelegate(); 
  public RestartSceneDelegate HandleRestartScene;

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

  public void RestartScene(float waitTime){
    StartCoroutine(RestartSceneTimer(waitTime));
  }

  IEnumerator RestartSceneTimer(float waitTime){
    yield return new WaitForSeconds(waitTime);
    HandleRestartScene();
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

  public Vector3 FocalPoint(){
    if (focalObject){
      return focalObject.transform.position;
    } else {
      return new Vector3(0,0,0);
    }
  }
}
