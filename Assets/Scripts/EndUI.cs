using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class EndUI : MonoBehaviour {

	public Transform endUI;

	void Awake() {
		if (endUI == null){
			Debug.LogError ("No EndUI specified!");
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Jogador") {
			other.GetComponent<Controls1>().cameraFollows = false;
			Destroy (other.gameObject, 5f);
			endUI.gameObject.SetActive(true);
			endUI.GetComponent<Animator>().SetBool("Ended", true);
		}
	}

	public void Restart() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void Quit() {
		Quit ();
	}
}