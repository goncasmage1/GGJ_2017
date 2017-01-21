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
        transform.position = Vector3.Lerp(transform.position, destination.position, speed * 3.0f * Time.deltaTime);
    }
}
