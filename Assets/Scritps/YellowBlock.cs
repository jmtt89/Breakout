using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBlock : MonoBehaviour {
    private int touches = 2;
    private AudioSource _bounce;
    private ParticleSystem _particle;
    private Renderer _render;
    private Collider _collider;
    private AudioSource _destroy;

    // Use this for initialization
    void Start () {
        LevelManager.numInitialBlocks++;
        _render = GetComponent<Renderer>();
        _particle = GetComponent<ParticleSystem>();
        _collider = GetComponent<Collider>();
        var effects = GetComponents<AudioSource>();
        foreach(AudioSource audio in effects)
        {
            if (audio.clip.name.Contains("Jump"))
            {
                _bounce = audio;
            }
            else
            {
                _destroy = audio;
            }
        }
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
        touches--;
        if (touches == 0)
        {
            _collider.enabled = false;
            StartCoroutine(KillMe());
        }
        else
        {
            _bounce.Play();
            iTween.PunchScale(gameObject, iTween.Hash("x", .15, "easeType", "easeInOutElastic", "loopType", "none", "delay", .05));
        }
    }

    IEnumerator KillMe()
    {
        _particle.Play();
        _destroy.Play();
        _render.enabled = false;
        LevelManager.Points += 150;
        LevelManager.numInitialBlocks--;
        yield return new WaitWhile(() => _destroy.isPlaying);
        yield return new WaitWhile(() => _particle.isPlaying);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        LevelManager.numInitialBlocks--;
    }

}
