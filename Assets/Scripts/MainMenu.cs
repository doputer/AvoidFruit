using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	GameObject soundManager;
	AudioSource audioSource;

	void Start() {
		soundManager = GameObject.Find("SoundManager");
		audioSource = soundManager.GetComponentInChildren<AudioSource>();
	}

	public void PlayGame() {
		PlayerPrefs.SetFloat("volume",audioSource.volume);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitGame() {
		Application.Quit();
	}
	
}
