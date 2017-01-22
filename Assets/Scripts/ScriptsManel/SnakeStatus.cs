using UnityEngine;
using System.Collections;

public class SnakeStatus : MonoBehaviour {

    public int numberOfBodyParts = 0;
    public bool hasDivided = false;
    public int secondHead;

	void Awake() {
		Screen.SetResolution (600, 950, false);
	}

	// Use this for initialization
	void Start () {
        numberOfBodyParts = 0;
        hasDivided = false;
	}

}
