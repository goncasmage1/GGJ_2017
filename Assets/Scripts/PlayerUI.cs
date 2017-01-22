using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	static public PlayerUI PUI;
	public Text scoreText;

	void Awake ()
	{
		if (PUI == null) {
			PUI = this;
		}
		if (scoreText == null) {
			Debug.LogError("No Text component specified!");
		}
	}

	public void UpdateScore(int newScore) {
		scoreText.text = "" + newScore;
	}
}
