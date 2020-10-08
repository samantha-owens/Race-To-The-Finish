using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool gameOver;
    public bool gameStart;

    public TextMeshProUGUI winText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI p1WinText;
    public TextMeshProUGUI p2WinText;

    public TextMeshProUGUI threeText;
    public TextMeshProUGUI twoText;
    public TextMeshProUGUI oneText;
    public TextMeshProUGUI startText;

    public int p1Score = 0;
    public int p2Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        // set gameover and gamestart bools to false
        gameOver = false;
        gameStart = false;

        // get scores from previous game and load it onto win count
        p1Score = PlayerPrefs.GetInt("P1 score");
        p2Score = PlayerPrefs.GetInt("P2 score");

        // update win count UI
        p1WinText.text = "P1 WINS: " + p1Score;
        p2WinText.text = "P2 WINS: " + p2Score;

        // start the countdown on each restart
        StartCoroutine(StartCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        RestartGame();
    }

    // initiates game over for player 1
    public void GameOverP1()
    {
        gameOver = true;
        winText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);

        // updates P1 score and UI
        p1Score++;
        p1WinText.text = "P1 WINS: " + p1Score;
    }

    // initiates game over for player 2
    public void GameOverP2()
    {
        gameOver = true;
        winText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);

        // updates P2 score and UI
        p2Score++;
        p2WinText.text = "P2 WINS: " + p2Score;
    }

    // if the game is over and the 'r' key is pressed, the score will save and the game will restart
    public void RestartGame()
    {
        if (Input.GetKeyDown(KeyCode.R) && gameOver)
        {
            SaveScore();

            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    // save the scores to player preferences so they stay when the game is restarted
    public void SaveScore()
    {
        PlayerPrefs.SetInt("P1 score", p1Score);
        PlayerPrefs.SetInt("P2 score", p2Score);
        PlayerPrefs.Save();
    }

    // starts visual countdown to the start of the race, enable player controls on 'start'
    IEnumerator StartCountdown()
    {
        threeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        threeText.gameObject.SetActive(false);

        twoText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        twoText.gameObject.SetActive(false);

        oneText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        oneText.gameObject.SetActive(false);

        startText.gameObject.SetActive(true);
        gameStart = true;
        yield return new WaitForSeconds(3);
        startText.gameObject.SetActive(false);
    }
}
