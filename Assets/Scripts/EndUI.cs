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
	public float wait = .1f;

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
			other.GetComponent<Controls1>().speed = 0;
			StartCoroutine(StartFlash (other));
		}
	}

	public IEnumerator StartFlash(Collider2D other) {
		flashUI.SetFlash (true);
		int i;
		for (i = 0; i < toDestroy.Length; i++) {
			yield return new WaitForSeconds (wait);
			Destroy (toDestroy [i].gameObject);
			yield return new WaitForSeconds (length - wait);
		}
		EnableUI (other);
		flashUI.SetFlash (false);
		yield break;
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