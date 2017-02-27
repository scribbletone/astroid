using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
  private GameManager game;
  private Vector3 focalPoint;
  private Vector3 offset;
  private int shakeCount = 0;
  private float shakeIntensity = 0.1f;

  void Start () 
  {
    game = GameManager.instance;

    UpdateFocalPoint();

    transform.position = new Vector3(focalPoint.x, 
    focalPoint.y, -10f);
    offset = transform.position - focalPoint;
    GameManager.majorDamageTakenEvent += HandleDamageTaken;
  }

  void OnDestroy() {     GameManager.majorDamageTakenEvent -= HandleDamageTaken;
  }
  
  void UpdateFocalPoint(){
    focalPoint = game.FocalPoint();

    if (shakeCount > 0){
      float randX = Random.Range(-shakeIntensity, shakeIntensity);
      float randY = Random.Range(-shakeIntensity, shakeIntensity);

      focalPoint = new Vector3(focalPoint.x + randX, focalPoint.y + randY, focalPoint.z);
      shakeCount -= 1;
    }
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

  void HandleDamageTaken(){
    shakeCount = 10;
  }
}