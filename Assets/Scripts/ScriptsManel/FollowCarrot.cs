using UnityEngine;
using System.Collections;

public class FollowCarrot : MonoBehaviour {

    public float speed;
    public Transform destination;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if (transform != null) {
			transform.position = Vector3.Lerp (transform.position, destination.position, speed * 3.0f * Time.deltaTime);
			Vector3 dir = transform.position - destination.transform.position;
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg + 180;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * speed);
		}
    }
}
