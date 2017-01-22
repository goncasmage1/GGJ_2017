using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MixerScript : MonoBehaviour {

	public AudioClip[] clips;
	private AudioSource source;
	public static MixerScript mixer;

	private int index = 0;
	public int loopStart;

	void Awake ()
	{
		if (mixer == null) {
			mixer = this;
		}
		if ((source = GetComponent<AudioSource> ()) == null) {
			Debug.Log ("No AudioSource available!");
		}
	}

	void Start ()
	{
		source.clip = clips [index];
		source.Play ();
		StartCoroutine(PlayAfterSound(clips[index].length));
	}

	IEnumerator PlayAfterSound (float secondsToWait)
	{
		yield return new WaitForSeconds (secondsToWait);
		index++;
		if (clips.Length > index) {
			source.clip = clips [index];
			source.Play ();
			StartCoroutine (PlayAfterSound (clips [index].length));
		} else {
			index = loopStart;
			source.clip = clips [index];
			source.Play ();
			StartCoroutine (PlayAfterSound (clips [index].length));
		}
		yield break;
	}

}
