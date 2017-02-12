using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

  public float rotationSpeed = 1f;
  private Vector3 amount;

  void Start(){
    amount = new Vector3(0,0,(100 * rotationSpeed));
  }

  void Update () {
    transform.Rotate(amount * Time.deltaTime);
  }
}
