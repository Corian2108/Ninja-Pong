using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{
    public Transform ball;
    public DeadZone playerScore;
    private Animator animator;
    public Rigidbody2D RbEnemy;
    public float speed;
    public float factor;
    public float visionRadius;   
    private float contdown; 

    private void Awake() 
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (ball.GetComponent<BallBehaviour>().gameStarted) {
            if (transform.position.y < ball.position.y) {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
            } else if (transform.position.y > ball.position.y) {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
            }
        }

        if (playerScore.scored) speed = speed + factor;
        
        float distance = Vector3.Distance(ball.position, transform.position);

        if (contdown > 0) contdown = contdown - 1 * Time.deltaTime;

        if(distance < visionRadius && contdown <= 0)
        {
            animator.SetTrigger("enemySlash");
            contdown = 0.5f;
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere((new Vector3(transform.position.x - 0.4f, transform.position.y, transform.position.z)), visionRadius);
    }

    public void enemyHurt()
    {
        animator.SetTrigger("enemyHurt");
    }

    public void enemyDie()
    {
        //Activar gravedad par que caiga
        RbEnemy.bodyType = RigidbodyType2D.Dynamic;
        animator.SetTrigger("enemyDie");
    }
}
