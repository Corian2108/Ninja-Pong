using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {
    public Rigidbody2D RbBall;
    public Transform paddle;
    public AudioSource ballAudio;
    public SpriteRenderer sprite;
    public bool gameStarted;
    float posDif = 0;
    public float maxDownSpeed = -4f;
    public float maxUpSpeed = 4f;
    public float maxDownAngle = -75f;
    public float maxUpAngle = 75f;
    float angle = 0;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        posDif = paddle.position.x - transform.position.x;
    }

    void Update () {
        if (!gameStarted) {
            transform.position = new Vector3 (paddle.position.x - posDif, paddle.position.y, paddle.position.z);
            if (Input.GetMouseButtonDown (0)) {
                RbBall.velocity = new Vector2 (8, 8);
                gameStarted = true;
            }
        }

        if (gameStarted && RbBall != null)
        {
            float currenVelocity = Mathf.Clamp(RbBall.velocity.y, maxDownSpeed, maxUpSpeed);
            if (RbBall.velocity.y > 0)
            {
                angle = (currenVelocity / maxUpSpeed) * maxUpAngle;
            }
            else
            {
                angle = (currenVelocity / maxDownSpeed) * maxDownAngle;
            }
            if (sprite.flipX)
            {
                angle = angle * -1;
            }
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = rotation;
        }
    }

    private void OnCollisionEnter2D(Collision2D ball) 
    {
        ballAudio.Play();
        if (ball.gameObject.tag == "Enemy") sprite.flipX = true;
        if (ball.gameObject.tag == "Player") sprite.flipX = false;
    }
}