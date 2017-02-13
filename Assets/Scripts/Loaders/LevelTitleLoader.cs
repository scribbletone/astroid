using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelTitleLoader : MonoBehaviour {

  void Update(){
    ReadyListener();
  }

  void ReadyListener(){
    if (Control.IsPressed("thrust"))
     {
       SceneManager.LoadScene("Level");
     }
  }

}
