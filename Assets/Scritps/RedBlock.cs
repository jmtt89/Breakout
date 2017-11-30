using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlock : MonoBehaviour {
    private AudioSource _destroy;
    private ParticleSystem _particle;
    private Renderer _render;
    private Collider _collider;

    // Use this for initialization
    void Start () {
        LevelManager.numInitialBlocks++;
        _collider = GetComponent<Collider>();
        _particle = GetComponent<ParticleSystem>();
        _render = GetComponent<Renderer>();
        _destroy = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.LeftControl))
        {
            StartCoroutine(KillMe());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        _collider.enabled = false;
        StartCoroutine(KillMe());
    }

    IEnumerator KillMe()
    {
        _particle.Play();
        _destroy.Play();
        _render.enabled = false;
        LevelManager.Points += 50;
        yield return new WaitWhile(() => _destroy.isPlaying);
        yield return new WaitWhile(() => _particle.isPlaying);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        LevelManager.numInitialBlocks--;
    }
}
