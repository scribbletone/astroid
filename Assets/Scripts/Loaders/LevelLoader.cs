using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
  public GameObject ship;
  public GameObject shipSpawnPointObject;
  public GameObject coins;
  public GameObject forceFields;

  private GameManager game;

  void Start(){
    game = GameManager.instance;
    SpawnShip();
    game.ResetAttributes();
    GameManager.resetSceneEvent += Reset;
  }

  void OnDestroy() {
    GameManager.resetSceneEvent -= Reset;
  }

  void Update(){
    ResetListener();
  }

  void Reset(){
    // ResetCoins();
    // ResetForceFields();
    // ship.GetComponent<Ship>().Reset();
    game.ResetAttributes();
  }

  void ResetListener(){
    if (Control.IsPressed("restart"))
     {
       game.ResetScene(0);
     }
  }

  void ResetCoins(){
    Coin[] coinList = coins.GetComponentsInChildren<Coin>(true);

    foreach(Coin coin in coinList) {
      coin.Reset();
    }
  }

  void ResetForceFields(){
    ForceField[] forceFieldList = forceFields.GetComponentsInChildren<ForceField>(true);
    foreach(ForceField forceField in forceFieldList) {
      forceField.Reset();
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
