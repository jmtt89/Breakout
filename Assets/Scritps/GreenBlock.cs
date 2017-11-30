using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBlock : MonoBehaviour {
    private int touches = 1;
    private PowerUp powerUpSc;
    private Renderer _render;
    private ParticleSystem _particle;
    private Collider _collider;
    private AudioSource _destroy;

    // Use this for initialization
    void Start () {
        powerUpSc = transform.parent.GetComponentInChildren<PowerUp>();
        LevelManager.numInitialBlocks++;
        _collider = GetComponent<Collider>();
        _particle = GetComponent<ParticleSystem>();
        _render = GetComponent<Renderer>();
        _destroy = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        touches--;
        if (touches == 0)
        {
            _collider.enabled = false;
            StartCoroutine(KillMe());
        }
    }

    IEnumerator KillMe()
    {
        _destroy.Play();
        _particle.Play();
        _render.enabled = false;
        LevelManager.Points += 30;
        powerUpSc.StartMovement();
        LevelManager.numInitialBlocks--;
        yield return new WaitWhile(() => _destroy.isPlaying);
        yield return new WaitWhile(() => _particle.isPlaying);
        Destroy(gameObject);
    }
}
