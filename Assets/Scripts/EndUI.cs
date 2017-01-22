using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class EndUI : MonoBehaviour {

	public Transform endUI;
	public FlashScript flashUI;
	public Transform[] toDestroy;
	public float length = 1f;

	void Awake() {
		if (endUI == null){
			Debug.LogError ("No EndUI specified!");
		}
		if (flashUI == null){
			Debug.LogError ("No FlashUI specified!");
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Jogador") {
			other.GetComponent<Controls1>().cameraFollows = false;
			StartFlash (other);
		}
	}

	public IEnumerator StartFlash(Collider2D other) {
		flashUI.SetFlash ();
		int i;
		for (i = 0; i < toDestroy.Length; i++) {

		}
	}

	public void EnableUI(Collider2D other) {
		endUI.gameObject.SetActive(true);
		GameObject texto = GameObject.FindGameObjectWithTag ("TextoMorte");
		if (texto != null) {
			texto.GetComponent<Text> ().text = "" + GameObject.FindGameObjectWithTag ("Player").GetComponent<SnakeStatus>().numberOfBodyParts;
		}
		endUI.GetComponent<Animator>().SetBool("Ended", true);
	}

	public void Restart() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void Quit() {
		Application.Quit ();
	}
}