using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
  public GameObject ship;
  public GameObject shipSpawnPointObject;
  private GameManager game;

  void Start(){
    game = GameManager.instance;
    SpawnShip();
    game.ResetAttributes();
    game.HandleRestartScene = Restart;
  }

  void Update(){
    RestartListener();
  }

  void Restart(){
    ReactivateItems();
    ship.GetComponent<Ship>().Reset();
    game.ResetAttributes();
  }

  void RestartListener(){
    if (Control.IsPressed("restart"))
     {
       game.RestartScene(0);
     }
  }

  void ReactivateItems(){
    for (int i = 0; i < game.coins.Count; i++) {
      game.coins[i].SetActive(true);
    }
  }

  void SpawnShip(){
    InitSpawnPoint();
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
