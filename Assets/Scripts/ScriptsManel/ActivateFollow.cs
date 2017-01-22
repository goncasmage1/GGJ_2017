using UnityEngine;
using System.Collections;

public class ActivateFollow : MonoBehaviour {

	bool invisivel;
	public int bonusScore = 5;
	int score = 0;

	private PlayerUI playerUI;

	void Start () {
		invisivel = true;
		playerUI = PlayerUI.PUI;
		if (transform.parent.name == "Snake") {
			if (playerUI == null) {
				Debug.LogError ("No playerUI available!");
			} else {
				playerUI.UpdateScore (score);
			}
		}
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
			Color tempColor = other.GetComponent<SpriteRenderer> ().color;
			tempColor.a = 255;
			other.GetComponent<SpriteRenderer> ().color = tempColor;

			score = GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeStatus>().numberOfBodyParts * bonusScore;
        }
		if (tag == "Jogador") {
			playerUI.UpdateScore(score);
			if (GameObject.FindGameObjectWithTag ("Player").GetComponent<SnakeStatus> ().numberOfBodyParts < 10) {
				GetComponent<Controls1> ().speed2 = 5f + 7f * (GameObject.FindGameObjectWithTag ("Player").GetComponent<SnakeStatus> ().numberOfBodyParts / 10f);
			} else {
				GetComponent<Controls1> ().speed2 = 12f;
			}
		}
		if (other.transform.tag == "Divide") {//CODIGO DAS BARREIRAS
			GameObject.Find ("bodyPart0").GetComponent<Controls1> ().divideBool = true;
			GameObject auxObjs = GameObject.Find("HbodyPart" + transform.parent.GetComponent<SnakeStatus>().secondHead.ToString());
			if(auxObjs!=null){auxObjs.GetComponent<Controls1> ().divideBool = true;}
			other.transform.tag = "UnDivide";
		}

    }

	public void UpdateScore(int newScore) {
		if (tag == "Jogador") {
			playerUI.UpdateScore (newScore * bonusScore);
			if (GameObject.FindGameObjectWithTag ("Player").GetComponent<SnakeStatus> ().numberOfBodyParts < 10) {
				GetComponent<Controls1> ().speed2 = 5f + 3f * (GameObject.FindGameObjectWithTag ("Player").GetComponent<SnakeStatus> ().numberOfBodyParts / 10f);
			} else {
				GetComponent<Controls1> ().speed2 = 8f;
			}
		}
	}

}
