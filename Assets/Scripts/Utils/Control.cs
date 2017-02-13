using UnityEngine;

public static class Control {

  public static bool IsPressed(string keyName){
    switch (keyName)
    {
      case "thrust":
        return Input.GetKey("joystick button 1") || Input.GetKey ("space");
      case "spinLeft":
        return Input.GetAxis("Horizontal") < 0;
      case "spinRight":
        return Input.GetAxis("Horizontal") > 0;
      case "restart":
        return Input.GetKeyDown(KeyCode.R) || Input.GetKey("joystick button 13");
      default:
        return false;
    }
  }

  public static float VerticalVal(){
    return Input.GetAxis ("Vertical");
  }

  public static float HorizontalVal(){
    return Input.GetAxis ("Horizontal");
  }

}