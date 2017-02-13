using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTitleUI : MonoBehaviour {

  public Text titleText;
  private GameManager game;

	void Start () {
    game = GameManager.instance;
		SetTitleText();
	}

  void SetTitleText(){
    titleText.text = "Level " + game.level;
  }
	
}
