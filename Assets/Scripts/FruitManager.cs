using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour {

	public Transform prefab;
	private float time;
	private float coefficient;

	void Start() {
		time = 0.1f;
		StartCoroutine("makeFruit");
	}
	
	IEnumerator makeFruit() {
		Instantiate(prefab,new Vector3(0,0,0),Quaternion.identity);

		coefficient = Random.Range(1,5);
		yield return new WaitForSeconds(time * coefficient);

		StartCoroutine("makeFruit");
	}
}
