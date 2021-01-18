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

    public bool scored;

    int scorePlayerQuantity;
    int scoreEnemyQuantity;

    private void OnTriggerEnter2D (Collider2D collision) {
        if (gameObject.tag == "Left") {
            scoreEnemyQuantity++;
            UpdateScoreLabel (scoreEnemyText, scoreEnemyQuantity);
        } else if (gameObject.CompareTag ("Right")) {
            scorePlayerQuantity++;
            UpdateScoreLabel (scorePlayerText, scorePlayerQuantity);
            scored = true;
        }

        collision.GetComponent<BallBehaviour>().gameStarted = false;
        scored = false;
        CheckScore();
        pointAudio.Play();
    }

    void CheckScore () {
        if (scorePlayerQuantity == 3) {
            sceneChanger.ChangeSceneTo ("WinScene");
        } else if (scoreEnemyQuantity == 3) {
            sceneChanger.ChangeSceneTo ("LoseScene");
        }
    }

    void UpdateScoreLabel (Text label, int score) {
        label.text = score.ToString ();
    }
}