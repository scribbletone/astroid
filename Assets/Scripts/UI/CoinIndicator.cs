using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinIndicator : MonoBehaviour {

  public string color;
  
  private GameManager game;

  void OnDestroy() {
     GameManager.coinAddedEvent -= HandleCoinAdded;
     GameManager.resetSceneEvent -= Reset;
  }

  void Start (){
    game = GameManager.instance;
    GameManager.coinAddedEvent += HandleCoinAdded;
    GameManager.resetSceneEvent += Reset;
    Reset();
  }

	void Reset(){
    gameObject.SetActive(false);
  }

  void HandleCoinAdded(GameObject newCoin){
    Reset();
    foreach (GameObject coin in game.coins) {
      string coinColor = coin.GetComponent<Coin>().color;
      if (coinColor == color) {
        gameObject.SetActive(true);
      }
    }
  }
}
