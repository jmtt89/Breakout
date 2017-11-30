using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour {
    private const float leftLimit = -4.5f;
    private const float rightLimit = 4.5f;
    public float maxSpeed = 3.0f;

    private Vector3 velocity;
    private GameObject _ball;

	// Use this for initialization
	void Start () {
        _ball = GameObject.Find("Ball");
        velocity = Vector3.zero;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(_ball != null)
        {
            transform.position += velocity * Time.deltaTime;

            var nVelocity = _ball.transform.position - transform.position;
            nVelocity = new Vector3(nVelocity.x, 0, 0);
            nVelocity.Normalize();
            nVelocity *= maxSpeed;

            velocity += nVelocity * Time.deltaTime;

        }
	}
}
