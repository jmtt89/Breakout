using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    private const float leftLimit = -4.5f;
    private const float rightLimit = 4.5f;
    private bool isExtra;
    public float speed = 3.0f;
    public int timeBigSize = 15;

    private SphereCollider myCollider;

    // Use this for initialization
    void Start() {
        isExtra = false;
        myCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0, 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
        transform.position.y,
        transform.position.z);
    }

    public void ActiveLongPaddle()
    {
        if (!isExtra)
        {
            StartCoroutine(LengthenPaddle());
        }
    }

    public void LaunchMultiBall()
    {

    }

    IEnumerator LengthenPaddle()
    {
        isExtra = true;
        var originalCenter = myCollider.center;
        transform.localScale += new Vector3(1.0f, 0, 0);
        myCollider.center = new Vector3(0.0f, 0.0f, -19.0f);
        yield return new WaitForSeconds(timeBigSize);
        transform.localScale -= new Vector3(1.0f, 0, 0);
        myCollider.center = originalCenter;
        isExtra = false;
    }

}
