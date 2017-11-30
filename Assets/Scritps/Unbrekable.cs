using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unbrekable : MonoBehaviour {
    private AudioSource _destroy;

    // Use this for initialization
    void Start () {
        _destroy = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        _destroy.Play();
    }
}
