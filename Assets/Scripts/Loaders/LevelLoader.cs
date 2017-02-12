using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
  public GameObject ship;
  public GameObject shipSpawnPointObject;
  private GameManager game;


  void Start(){
    game = GameManager.instance;
    SpawnShip();
  }

  void Update(){
    RestartListener();
  }

  void Restart(){
    ReactivateItems();
    game.ResetAttributes();
    ship.GetComponent<Ship>().Reset();
  }

  void RestartListener(){
    if( Input.GetKeyDown(KeyCode.R) || Input.GetKey("joystick button 13") )
     {
       Restart();
     }
  }

  void ReactivateItems(){
    for (int i = 0; i < game.coins.Count; i++) {
      game.coins[i].SetActive(true);
    }
  }

  void SpawnShip(){
    if (game.shipSpawnPoint == new Vector3(0,0,0)) {
      InitSpawnPoint();
    }
    ship = Instantiate(ship, game.shipSpawnPoint, Quaternion.identity);
    game.focalObject = ship;
  }

  void InitSpawnPoint(){
    Vector3 spawnPoint = new Vector3(5,5, 0);

    if (shipSpawnPointObject) {
      spawnPoint = new Vector2(shipSpawnPointObject.transform.position.x, shipSpawnPointObject.transform.position.y);

      Destroy(shipSpawnPointObject);
    }

    game.shipSpawnPoint = spawnPoint;
  }
  
}
