using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FlashScript : MonoBehaviour {

	private Animator anim;

	public void Awake() {
		if ((anim == GetComponent<Animator> ()) == null) {
			Debug.LogError ("No Animator available");
		}
	}

	public void SetFlash(bool newFlash) {
		anim.SetBool ("Flash", newFlash);
	}
}
