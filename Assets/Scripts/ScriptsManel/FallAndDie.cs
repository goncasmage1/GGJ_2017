using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAndDie : MonoBehaviour {
	bool canCollide = true;

	// Use this for initialization
	void Start () {
		canCollide = true;
	}
	
	// Update is called once per frame
	void Update () {
		try{}
		catch(MissingReferenceException e){
			Destroy(gameObject);}
		}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.tag == "Level" && canCollide) {

			canCollide = false;
			int count = 0;
			while(gameObject.name != "bodyPart" + count.ToString()){count++;}


			gameObject.GetComponent<FollowCarrot> ().enabled = false;
			gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
			for (int j = GameObject.FindGameObjectWithTag ("Player").GetComponent<SnakeStatus> ().numberOfBodyParts; j >= count; j--) {
				if(GameObject.Find("bodyPart"+j) != null)
					Destroy (GameObject.Find("bodyPart"+j));
			}

			GameObject.FindGameObjectWithTag ("Player").GetComponent<SnakeStatus> ().numberOfBodyParts 
			= (count - 1 > GameObject.FindGameObjectWithTag ("Player").GetComponent<SnakeStatus> ().numberOfBodyParts)
				? GameObject.FindGameObjectWithTag ("Player").GetComponent<SnakeStatus> ().numberOfBodyParts : count - 1; 
		}
	}

}
