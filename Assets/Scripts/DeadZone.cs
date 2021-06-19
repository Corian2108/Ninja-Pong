using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadZone : MonoBehaviour {
    public Text scorePlayerText;
    public Text scoreEnemyText;
    public SceneChanger sceneChanger;
    public AudioSource pointAudio;
    public PaddleMovement playerAnim;
    public FollowBall enemyAnim;
    public SpriteRenderer ball;

    public bool scored;
    private float contdown;
    int scorePlayerQuantity;
    int scoreEnemyQuantity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Left") {
            scoreEnemyQuantity++;
            UpdateScoreLabel (scoreEnemyText, scoreEnemyQuantity);
            playerAnim.playerHurt();
            ball.flipX = false;
        } else if (gameObject.CompareTag ("Right")) {
            scorePlayerQuantity++;
            UpdateScoreLabel (scorePlayerText, scorePlayerQuantity);
            enemyAnim.enemyHurt();
            scored = true;
            ball.flipX = false;
        }

        collision.GetComponent<BallBehaviour>().gameStarted = false;
        collision.transform.rotation = Quaternion.Euler(0, 0, 0);
        scored = false;
        CheckScore();
        pointAudio.Play();
    }

    void CheckScore () {
        if (scorePlayerQuantity == 3) {
            contdown = 1f;
            enemyAnim.enemyHurt();
            enemyAnim.enemyDie();
        } else if (scoreEnemyQuantity == 3) {
            contdown = 1f;
            playerAnim.playerHurt();
            playerAnim.playerDie();
        }
    }

    void UpdateScoreLabel (Text label, int score) {
        label.text = score.ToString ();
    }

    private void Update()
    {
        if (contdown >= 0)
        {
            contdown = contdown - 1 * Time.deltaTime;
        }
        else if (contdown <= 0)
        {
            if (scorePlayerQuantity == 3)
            {
                sceneChanger.ChangeSceneTo("WinScene");
            }
            else if (scoreEnemyQuantity == 3)
            {
                sceneChanger.ChangeSceneTo("LoseScene");
            }
        }
    }
}