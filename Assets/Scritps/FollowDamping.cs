using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDamping : MonoBehaviour {
    public GameObject target;
    public float damping = 0.5f;
    Vector3 offset;

    // Use this for initialization
    void Start () {
        offset = transform.position - target.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {
        Vector3 desiredPosition = target.transform.position + offset;
        Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
        transform.position = position;
    }
}
