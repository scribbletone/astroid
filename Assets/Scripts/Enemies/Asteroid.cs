using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

  public float damage = -2f;

  private GameManager game;
  private Vector3 spawnPoint;
  private Rigidbody2D rigidAsteroid;

	void Start () {
    game = GameManager.instance;
    spawnPoint = transform.position;
    rigidAsteroid = GetComponent<Rigidbody2D>();
	}

  public void Reset(){
    rigidAsteroid.velocity = Vector3.zero;
    rigidAsteroid.angularVelocity = 0;
    transform.eulerAngles = new Vector3(0, 0, 0);
    transform.position = spawnPoint;
  }

  void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.tag == "Player") {
      other.gameObject.GetComponent<Ship>().AdjustHull(damage);
      print(game.hull);
    }
  }
}
