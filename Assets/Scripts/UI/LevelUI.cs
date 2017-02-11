using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelUI : MonoBehaviour {
  public Text fuelText;
  public Text clockText;
  private GameManager game;

  void Start (){
    game = GameManager.instance;
  }

  void Update () {
    UpdateFuel();
    UpdateClock();
  }

  void UpdateFuel() {
    if (fuelText) {
      fuelText.text = "Fuel: " + Math.Round(game.fuel, 0) + "/" + game.maxFuel;
    }
  }

  void UpdateClock() {
    if (clockText) {
      clockText.text = "Time: " + Math.Round(game.clock, 0);
    }
  }
}
