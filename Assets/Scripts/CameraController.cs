using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
  private GameManager game;
  private Vector3 focalPoint;
  private Vector3 offset;

  void Start () 
  {
    game = GameManager.instance;

    UpdateFocalPoint();

    transform.position = new Vector3(focalPoint.x, 
    focalPoint.y, -10f);
    offset = transform.position - focalPoint;
  }
  
  void UpdateFocalPoint(){
    focalPoint = game.FocalPoint();
  }

  void LateUpdate () 
  {
    UpdateFocalPoint();
    transform.position = focalPoint + offset;
  }

  public static float RoundToNearestPixel(float unityUnits, Camera viewingCamera)
  {
      float valueInPixels = (Screen.height / (viewingCamera.orthographicSize * 2)) * unityUnits;
      valueInPixels = Mathf.Round(valueInPixels);
      float adjustedUnityUnits = valueInPixels / (Screen.height / (viewingCamera.orthographicSize * 2));
      return adjustedUnityUnits;
  }
}