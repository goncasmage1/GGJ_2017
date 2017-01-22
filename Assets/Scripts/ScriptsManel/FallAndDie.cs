using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallAndDie : MonoBehaviour {

	public Transform deathParticles;

	bool canCollide = true;
	bool isPlayer = false;

	void Awake() {
		if (deathParticles == null && tag == "Jogador") {
			Debug.LogError ("No DeathParticles specified!");
		}
	}

	// Use this for initialization
	void Start () {
		canCollide = true;
	}
	
	// Update is called once per frame
    void Update() { }
		

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.tag == "Level" && canCollide) {
			canCollide = false;
			int count = 0;

			if (tag == "Jogador") {
				Debug.Log ("Morreu");
				isPlayer = true;
			}
            if (!gameObject.name.Contains("H"))
            {
                while (gameObject.name != "bodyPart" + count.ToString())
                {
                    count++;
                }
                gameObject.GetComponent<FollowCarrot>().enabled = false;
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                int secondSnake = (GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeStatus>().hasDivided) ? (GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeStatus>().secondHead) : 0;
                for (int j = GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeStatus>().numberOfBodyParts - secondSnake; j >= count; j--)
                {
					if (j == 0) {
						Transform clone = Instantiate (deathParticles, transform.position, transform.rotation);
						Destroy (clone.gameObject, 5f);
						Invoke ("Restart", 2f);
						GetComponent<SpriteRenderer> ().enabled = false;
						GetComponent<Controls1> ().enabled = false;
					}
                    else if (GameObject.Find("bodyPart" + j) != null)
                        Destroy(GameObject.Find("bodyPart" + j));
                }

                GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeStatus>().numberOfBodyParts
                = (count - 1 > GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeStatus>().numberOfBodyParts)
                    ? GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeStatus>().numberOfBodyParts : count - 1;

				if (count > 0) {
					GameObject.FindGameObjectWithTag ("Jogador").GetComponent<ActivateFollow> ().UpdateScore (count - 1);
				}
            }
            else
            {
                for (int j = GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeStatus>().numberOfBodyParts; j >
                    GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeStatus>().secondHead; j--) 
                {
                    if (GameObject.Find("bodyPart" + j) != null)
                        Destroy(GameObject.Find("bodyPart" + j));
                }
                GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeStatus>().numberOfBodyParts
                -= GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeStatus>().secondHead;
				Invoke ("Restart", 2f);
				GetComponent<SpriteRenderer> ().enabled = false;
				GameObject.FindGameObjectWithTag("Jogador").GetComponent<Controls1> ().cameraFollows = false;
            }
           
           
		}
	}

	private void Restart() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

}
