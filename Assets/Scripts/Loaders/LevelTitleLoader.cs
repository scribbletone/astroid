using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelTitleLoader : MonoBehaviour {
  private GameManager game;

  void Update(){
    game = GameManager.instance;
    ReadyListener();
  }

  void ReadyListener(){
    if (Control.IsPressed("thrust"))
     {
       SceneManager.LoadScene("Level_" + game.level);
     }
  }

}
