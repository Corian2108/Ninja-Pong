using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {
    public Rigidbody2D RbBall;
    public Transform paddle;
    public bool gameStarted;
    public AudioSource ballAudio;
    float posDif = 0;
    // Start is called before the first frame update
    void Start () {
        posDif = paddle.position.x - transform.position.x;
    }

    // Update is called once per frame
    void Update () {
        if (!gameStarted) {
            transform.position = new Vector3 (paddle.position.x - posDif, paddle.position.y, paddle.position.z);
            if (Input.GetMouseButtonDown (0)) {
                RbBall.velocity = new Vector2 (8, 8);
                gameStarted = true;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D ball) 
    {
        ballAudio.Play();
    }
}