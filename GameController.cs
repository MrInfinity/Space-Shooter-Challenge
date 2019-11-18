using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text RestartText;
    public Text GameOverText;
    public Text WinText;

    private bool gameOver;
    private bool restart;
    private int score;

    void Start ()
    {
        gameOver = false;
        restart = false;
        RestartText.text = "";
        GameOverText.text = "";
        WinText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves ());
    }

    void Update ()
    {
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.L))
            {
                SceneManager.LoadScene("Current");
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves ()
    {
            yield return new WaitForSeconds (startWait);
            while (true)
            {
                for (int i = 0; i < hazardCount; i++)
                {

                    GameObject hazard = hazards[Random.Range (0,hazards.Length)];
                    Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion SpawnRotation = Quaternion.identity;
                    Instantiate (hazard, spawnPosition, SpawnRotation);
                    yield return new WaitForSeconds (spawnWait);
                }
                yield return new WaitForSeconds (waveWait);

                if (gameOver)
                {
                    RestartText.text = "Press 'L' to Restart";
                    restart = true;
                    break;
                }
            }

    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;

        if (score >= 100)
        {
            WinText.text = "You Win! \n Game Created by \n Austin Owens!";
            GameOver();
        }
    }

    public void GameOver ()
    {
        GameOverText.text = "Game Over!";
        gameOver = true;
    }
}
