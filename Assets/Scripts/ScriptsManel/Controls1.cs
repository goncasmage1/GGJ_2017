using UnityEngine;
using XInputDotNetPure;
using System.Collections;

public class Controls1 : MonoBehaviour {

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    public float speed = 1.0f, speed2;
	public float extraCameraX = 5f;
	public bool goUp;
	public int divide_number;
    bool flag = true;
	public bool divideBool = false;

	/// <summary>
	///  DEBUG
	/// </summary>
	public bool debugBool; 

    // Use this for initialization
    void Start()
    {
		if (divide_number < 1) {
			divide_number = 2;
		}

        // No need to initialize anything for the plugin
    }

    // Update is called once per frame
    void Update()
    {
		if (goUp) {
			Vector3 newPos = transform.position;
			newPos.x += speed2*Time.deltaTime;
			transform.position = newPos;
			if (transform.parent.GetComponent<SnakeStatus>().hasDivided) {
				GameObject auxObjs = GameObject.Find("HbodyPart" + transform.parent.GetComponent<SnakeStatus>().secondHead.ToString());
				Vector3 newPos2 = auxObjs.transform.position;
				newPos2.x = newPos.x;
				newPos2.x += speed2*Time.deltaTime;
				auxObjs.transform.position = newPos2;
			}
			Vector3 newCameraPos = Camera.main.transform.position;
			newCameraPos.x = transform.position.x + extraCameraX;
			Camera.main.transform.position = newCameraPos;
		}

        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);
        GameObject auxObj;
        float restrictMoveX = transform.position.x + state.ThumbSticks.Left.X * speed;
        bool forFlag = false;
        // Detect if a button was pressed this frame
		if ((prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed ) || divideBool  )
        {
			divideBool = false;
			if (transform.parent.GetComponent<SnakeStatus>().numberOfBodyParts > divide_number && !transform.parent.GetComponent<SnakeStatus>().hasDivided)
            {
				Debug.Log ("I am here 2 crl");
				flag = true;
			
                auxObj = GameObject.Find( "bodyPart" + (transform.parent.GetComponent<SnakeStatus>().numberOfBodyParts / 2 + 1).ToString());
                auxObj.GetComponent<FollowCarrot>().enabled = false;
                auxObj.transform.position = new Vector3(0,1,0);
                auxObj.name = "H" + auxObj.name;//secondary Head
                auxObj.GetComponent<Controls1>().enabled = true;
				auxObj.GetComponent<FallAndDie> ().enabled = false;
                transform.parent.GetComponent<SnakeStatus>().hasDivided = true;
                transform.parent.GetComponent<SnakeStatus>().secondHead = transform.parent.GetComponent<SnakeStatus>().numberOfBodyParts / 2 + 1;
            }
            else if (transform.parent.GetComponent<SnakeStatus>().hasDivided && gameObject.transform.name != "bodyPart0")
            {
				Debug.Log ("I am here");
                auxObj = GameObject.Find("HbodyPart" + transform.parent.GetComponent<SnakeStatus>().secondHead.ToString());
                auxObj.name = "bodyPart" + transform.parent.GetComponent<SnakeStatus>().secondHead.ToString();
                auxObj.GetComponent<FollowCarrot>().enabled = true;
                auxObj.GetComponent<Controls1>().enabled = false;
				auxObj.GetComponent<FallAndDie> ().enabled = true;

                for (int i = transform.parent.GetComponent<SnakeStatus>().secondHead - 1; i >= 0; i--)
                {
                    if (GameObject.Find("bodyPart" + i.ToString()) != null)
                    {
                        transform.parent.GetComponent<SnakeStatus>().hasDivided = false;
                        auxObj.GetComponent<FollowCarrot>().destination = GameObject.Find("bodyPart" + (i).ToString()).transform.GetChild(0).transform;
                        i = -1;
                    }
                }
            }
        }
        // Detect if a button was released this frame
		if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released || !debugBool)
        {
        }


        // Set vibration according to triggers
        GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);

        // Make the current object turn
        //transform.localRotation *= Quaternion.Euler(0.0f, state.ThumbSticks.Left.X * 25.0f * Time.deltaTime, 0.0f);
        if(transform.name == "bodyPart0")
			transform.position = new Vector3(transform.position.x, transform.position.y - state.ThumbSticks.Left.X * speed + Mathf.Cos(Time.time*10) * 0.05f, 0.0f);
        if (transform.name.Contains("HbodyPart") && transform.parent.GetComponent<SnakeStatus>().hasDivided == true)
        {
			transform.position = new Vector3(transform.position.x, transform.position.y - state.ThumbSticks.Right.X * speed + Mathf.Cos(Time.time * 10) * 0.05f, 0.0f);
        }
			
    }

    void OnGUI()
    {
        /*string text = "Use left stick to turn the cube, hold A to change color\n";
        text += string.Format("IsConnected {0} Packet #{1}\n", state.IsConnected, state.PacketNumber);
        text += string.Format("\tTriggers {0} {1}\n", state.Triggers.Left, state.Triggers.Right);
        text += string.Format("\tD-Pad {0} {1} {2} {3}\n", state.DPad.Up, state.DPad.Right, state.DPad.Down, state.DPad.Left);
        text += string.Format("\tButtons Start {0} Back {1} Guide {2}\n", state.Buttons.Start, state.Buttons.Back, state.Buttons.Guide);
        text += string.Format("\tButtons LeftStick {0} RightStick {1} LeftShoulder {2} RightShoulder {3}\n", state.Buttons.LeftStick, state.Buttons.RightStick, state.Buttons.LeftShoulder, state.Buttons.RightShoulder);
        text += string.Format("\tButtons A {0} B {1} X {2} Y {3}\n", state.Buttons.A, state.Buttons.B, state.Buttons.X, state.Buttons.Y);
        text += string.Format("\tSticks Left {0} {1} Right {2} {3}\n", state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);*/
		string text = "";
		text += string.Format ("debug Stuff here");
		GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);
    }
}
