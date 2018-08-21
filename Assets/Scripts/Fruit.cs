using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour {

	private float movePower = 5f;
	private float coefficient;
	private float randomX;

	void Start() {
		coefficient = Random.Range(1,2);
		randomX = Random.Range(-5,5);
		this.transform.position = new Vector3(randomX,7,0);
	}

	void Update() {
		Vector3 moveVelocity = Vector3.zero;

		moveVelocity = Vector3.down;
		transform.position += coefficient * moveVelocity * movePower * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Ground") {
			Destroy(this.gameObject);

			ScoreManager.setScore(1);
		}
	}
}
