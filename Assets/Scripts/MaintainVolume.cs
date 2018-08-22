using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainVolume : MonoBehaviour {

	AudioSource audioSource;

	void Start() {
		audioSource = gameObject.GetComponentInChildren<AudioSource>();

		float volume = PlayerPrefs.GetFloat("volume");
		audioSource.volume = volume;
	}

}
