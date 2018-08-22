﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Fruit") {
			Destroy(other.gameObject);

			ScoreManager.setScore(1);
		}
	}
}
