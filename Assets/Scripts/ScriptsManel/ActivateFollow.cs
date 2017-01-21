﻿using UnityEngine;
using System.Collections;

public class ActivateFollow : MonoBehaviour {
	bool invisivel;
	// Use this for initialization
	void Start () {
		invisivel = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name.Contains("bodyPart") && other.transform.parent.name != "Snake")
        {
			//other.GetComponent<SpriteRenderer> ().enabled = invisivel;
			//invisivel = !invisivel;
            other.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
            other.GetComponent<FollowCarrot>().enabled = true;
            other.GetComponent<FollowCarrot>().destination = GameObject.Find("bodyPart" + 
                                                                                   GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeStatus>().numberOfBodyParts.ToString()
                                                                                   ).transform.GetChild(0).transform;
            GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeStatus>().numberOfBodyParts++;
            other.transform.name = "bodyPart" + GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeStatus>().numberOfBodyParts.ToString();
         

        }
		if (other.transform.tag == "Divide") {//CODIGO DAS BARREIRAS
			GameObject.Find ("bodyPart0").GetComponent<Controls1> ().divideBool = true;
			GameObject auxObjs = GameObject.Find("HbodyPart" + transform.parent.GetComponent<SnakeStatus>().secondHead.ToString());
			if(auxObjs!=null){auxObjs.GetComponent<Controls1> ().divideBool = true;}
			other.transform.tag = "UnDivide";
		}

	

    }
}