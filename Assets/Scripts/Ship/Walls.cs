using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void OnCollisionEnter2D(Collision2D other) {
    // paramContainer.FindParam("paramName")
    print(other.gameObject.tag);
    // if (other.gameObject.tag == "Wall"){
    //   float damage = other.relativeVelocity.magnitude;
    //   damage = damage * damage * -1f;
    //   if (damage > -1)
    //     damage = 0;
    //   AdjustHull(damage);
    // }
    
  }
}
