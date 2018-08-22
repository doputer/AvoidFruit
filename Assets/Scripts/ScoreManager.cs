using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	private GUIStyle GUIStyle = new GUIStyle();

	static int score = 0;

	public static void setScore(int value) {
		score += value;
	}

	public static int getScore() {
		return score;
	}

	void OnGUI() {
		GUIStyle.fontSize = 70;
		GUILayout.BeginArea(new Rect(0,0,Screen.width,Screen.height));

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();

		GUILayout.Label("Score : " + score.ToString());

		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		GUILayout.EndArea();
	}
}
