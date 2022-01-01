using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsScript : MonoBehaviour
{
    public Text scoreText;
    public Text resultText;
    public Button menuButton;
    public Button retryButton;
    public NavigationScript navigationScript;
    public HighScoreScript highScoreScript;

    // Start is called before the first frame update
    void Start()
    {
        menuButton.onClick.AddListener(GotToMenu);
        retryButton.onClick.AddListener(Retry);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Retry();
    }

    private void Retry()
    {
        navigationScript.StartNewGame();
    }

    private void GotToMenu()
    {
        navigationScript.ReturnToMenu();
    }

    internal void UpdateResults(bool didPlayerWin, float time)
    {
        if (didPlayerWin)
        {
            //Updates and checks score then displays the appropriate message
            if(highScoreScript.IsNewHighScore(time))
                resultText.text = "New High Score!";
            else
                resultText.text = "Success!";
            string t = "You correctly answered in " + time.ToString("F2") + " Seconds.";
            scoreText.text = t;
        }
        else
        {
            resultText.text = "Failure!";
            scoreText.text = "Try Again?";
        }
    }
}
