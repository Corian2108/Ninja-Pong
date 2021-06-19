using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D RbPlayer;
    public float visionRadius;
    private float contdown;
    private bool isDead = false;
    GameObject ball;
    BallBehaviour gameStatus;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        gameStatus = ball.GetComponent<BallBehaviour>();
    }

    void Update()
    {
        if (!isDead)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(mousePos.y, -3.43f, 4.63f), transform.position.z);

            float distance = Vector3.Distance(ball.transform.position, transform.position);

            if (contdown > 0) contdown = contdown - 1 * Time.deltaTime;

            if (distance < visionRadius && gameStatus.gameStarted && contdown <= 0)
            {
                animator.SetTrigger("playerSlash");
                contdown = 0.5f;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((new Vector3(transform.position.x + 0.32f, transform.position.y - 0.6f, transform.position.z)), visionRadius);
    }

    public void playerHurt()
    {
        animator.SetTrigger("playerHurt");
    }

    public void playerDie()
    {
        isDead = true;
        //Cambiar rigidbody de Kinematic a Dynamic
        RbPlayer.bodyType = RigidbodyType2D.Dynamic;
        animator.SetTrigger("playerDie");
    }
}
