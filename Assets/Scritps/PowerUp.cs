using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUp : MonoBehaviour {
    private const float speedIncrement = 0.5f;
    private const float speedIncrementRate = 15.0f;
    private Rigidbody _rigidbody;
    private int _type;

    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _type = Random.Range(0, 3);
        var matetial = GetComponent<Renderer>().material;
        switch (_type)
        {
            case 0:
                matetial.color = Color.magenta;
                break;
            case 1:
                matetial.color = Color.red;
                break;
            case 2:
                matetial.color = Color.yellow;
                break;
        }
        enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartMovement()
    {
        Debug.Log("StartMovement");
        enabled = true;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(0.0f, 0.0f, -1.0f, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(transform.parent.gameObject);
        if (collider.name == "Paddle")
        {
            LevelManager.Points += 10;
            var paddleSc = collider.GetComponent<Paddle>();
            switch (_type)
            {
                case 0: // Alargar el Paddle
                    paddleSc.ActiveLongPaddle();
                    break;
                case 1: // Add Power Bomb
                    LevelManager.AddPowerBomb();
                    break;
                case 2: // Bonus * 2 por 15 seg
                    LevelManager.StartBonus();
                    break;
            }
            Destroy(transform.parent.gameObject);
        }
        else if (collider.tag == "Die")
        {
            Destroy(transform.parent.gameObject);
        }
    }

}
