using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Text highScore;
    public Button startGamebutton;
    public NavigationScript navigationScript;
    public HighScoreScript highScoreScript;

    // Start is called before the first frame update
    void Start()
    {
        startGamebutton.onClick.AddListener(StartGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartGame();
    }

    private void StartGame()
    {
        navigationScript.StartNewGame();
    }

    //gets the highscore
    private void OnEnable()
    {
        string score = highScoreScript.highScore.ToString("F2");
        highScore.text = "High Score: " + score + " seconds";
    }
}
