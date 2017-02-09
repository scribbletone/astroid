using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
  public GameObject ship;
  public GameObject shipSpawnPointObject;
  private GameManager gameManager;


  void Start(){
    gameManager = GameManager.instance;
    SpawnShip();
  }

  void SpawnShip(){
    if (gameManager.shipSpawnPoint == new Vector3(0,0,0)) {
      InitSpawnPoint();
    }

    ship = Instantiate(ship, gameManager.shipSpawnPoint, Quaternion.identity);
    gameManager.focalObject = ship;
  }

  void InitSpawnPoint(){
    Vector3 spawnPoint = new Vector3(5,5, 0);

    if (shipSpawnPointObject) {
      spawnPoint = new Vector2(shipSpawnPointObject.transform.position.x, shipSpawnPointObject.transform.position.y);

      Destroy(shipSpawnPointObject);
    }

    gameManager.shipSpawnPoint = spawnPoint;
  }
  
}
