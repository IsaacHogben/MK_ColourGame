using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationScript : MonoBehaviour
{
    public GameObject gameScreen;
    public GameObject menuScreen;
    public GameObject resultScreen;
    public ResultsScript resultsScript;

    // Start is called before the first frame update
    void Start()
    {
        ShowMenu();
    }

    public void ShowResultScreen(bool didPlayerWin, float time)
    {
        resultsScript.UpdateResults(didPlayerWin, time);
        ShowResults();        
    }

    public void StartNewGame()
    {
        ShowGame();
    }

    public void ReturnToMenu()
    {
        ShowMenu();
    }

    private void ShowMenu()
    {
        menuScreen.SetActive(true);
        gameScreen.SetActive(false);
        resultScreen.SetActive(false);
    }

    private void ShowGame()
    {
        menuScreen.SetActive(false);
        gameScreen.SetActive(true);
        resultScreen.SetActive(false);
    }

    private void ShowResults()
    {
        menuScreen.SetActive(false);
        gameScreen.SetActive(false);
        resultScreen.SetActive(true);
    }
}