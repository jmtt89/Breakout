using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private const float speedIncrement = 0.5f;
    private const float speedIncrementRate = 15.0f;
    private GameObject _paddle;
    private SphereCollider _collider;
    private ParticleSystem _particle;
    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private Color _original;

    // Use this for initialization
    void Start () {
        _paddle = GameObject.Find("Paddle");
        _renderer = GetComponent<Renderer>();
        _original = _renderer.material.color;
        _collider = GetComponent<SphereCollider>();
        _particle = GetComponent<ParticleSystem>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        RestartBall();
        StartCoroutine(IncreaseSpeed());
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump") && LevelManager.PowerBomb > 0)
        {
            StartCoroutine(Explode());
        }else if (Input.GetKeyDown(KeyCode.Escape))
        {
            RestartBall();
        }
    }

    void LateUpdate () {
        if (LevelManager.InBonusTime() && _renderer.material.color == _original)
        {
            Debug.Log("LATE_UPDATE");
            Debug.Log(LevelManager.InBonusTime());
            _renderer.material.color = Color.yellow;
        }
        else if(!LevelManager.InBonusTime() && _renderer.material.color != _original)
        {
            _renderer.material.color = _original;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].otherCollider.tag == "Die")
        {
            LevelManager.Points -= 25;
            RestartBall();
        }
            
    }

    void RestartBall()
    {
        transform.position = new Vector3(_paddle.transform.position.x, _paddle.transform.position.y, _paddle.transform.position.z + 0.5f );
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(Random.Range(-2.5f, 2.5f), 0.0f, Random.Range(1.0f, 2.5f), ForceMode.Impulse);
    }

    IEnumerator Explode()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        LevelManager.UsePowerBomb();
        _particle.Play();
        _collider.radius += 2.0f;
        yield return new WaitWhile(() => _particle.isPlaying);
        _collider.radius -= 2.0f;
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        RestartBall();
    }

    IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            _rigidbody.AddForce(_rigidbody.velocity.normalized * speedIncrement, ForceMode.Impulse);
            yield return new WaitForSeconds(speedIncrementRate);
        }
    }
}
