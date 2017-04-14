using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelUI : MonoBehaviour {
  public Text clockText;
  public Text coinsText;
  public Text hullText;
  public Slider fuelBar;
  public Slider hullBar;
  public GameObject coinIndicators;

  private GameManager game;

  void Start (){
    game = GameManager.instance;
  }
  
  void Update () {
    UpdateFuel();
    UpdateClock();
    UpdateCoins();
    UpdateHull();
  }

  void UpdateFuel() {
    if (fuelBar) {
      fuelBar.value = game.fuel / game.maxFuel;
    }
  }

  void UpdateClock() {
    if (clockText) {
      clockText.text = "Time: " + Math.Round(game.clock, 0);
    }
  }

  void UpdateCoins() {
    if (coinsText) {
      coinsText.text = "Coins: " + game.coins.Count;
    }
  }

  void UpdateHull() {
    if (hullBar) {
      hullBar.value = game.hull / game.maxHull;
    }
  }
}
