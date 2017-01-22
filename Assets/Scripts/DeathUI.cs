using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour {

	public void Awake ()
	{
		enabled = false;
	}

	public void OnDeath ()
	{
		enabled = true;
	}

	public void Retry() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Quit() {
		Quit();
	}
}
