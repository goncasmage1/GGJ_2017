using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerScript : MonoBehaviour {

	public AudioSource[] tracks;
	private int index = 0;
	public int loopStart;

	void Awake ()
	{
		if (tracks.Length == 0) {
			Debug.LogError("No audio sources indicated!");
		}
	}

	void Start ()
	{
		Instantiate(tracks[index]);
		StartCoroutine(PlayAfterSound(tracks[index].clip.length));
	}

	IEnumerator PlayAfterSound (float secondsToWait)
	{
		yield return new WaitForSeconds (secondsToWait);
		index++;
		if (tracks.Length > index) {
			Instantiate (tracks [index]);
			StartCoroutine (PlayAfterSound (tracks [index].clip.length));
		} else {
			index = loopStart;
			Instantiate (tracks [index]);
			StartCoroutine (PlayAfterSound (tracks [index].clip.length));
		}
		yield break;
	}
}
