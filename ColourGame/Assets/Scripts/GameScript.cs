using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameScript : MonoBehaviour
{
    //Using the public drag-n-drop method of referencing rather than GameObject.Find()
    public Text timerText;
    public Text colourText;
    public Button redButton;
    public Button blueButton;
    public Button greenButton;
    public Button pinkButton;
    //The float used to track time
    private float timer;
    //Colours
    private Color colourRed = Color.red;
    private Color colourBlue = Color.blue;
    private Color colourGreen = Color.green;
    private Color colourPink = new Color(255, 0, 51);
    //Color objects
    ColourScript csRed;
    ColourScript csBlue;
    ColourScript csGreen;
    ColourScript csPink;
    //list of colour scripts to select from
    public List<ColourScript> colourList;
    //previous word/colour combo to prevent repeats (one in sixteen chance)
    ColourScript csLast;
    ColourScript csNext;
    //A counter to track answers given
    int numAnswersGiven = 0;
    //number of questions asked, question being word/colour combos shown.
    public int numOfQuestions = 10;
    bool gameIsRunning = false;

    public NavigationScript navigationScript;

    // Awake is called when the script is initialized
    private void Awake()
    {
        //AddButton Listeners
        redButton.onClick.AddListener(redButtonClick);
        blueButton.onClick.AddListener(blueButtonClick);
        greenButton.onClick.AddListener(greenButtonClick);
        pinkButton.onClick.AddListener(pinkButtonClick);
        //initialize colour objects
        csRed = new ColourScript("Red", colourRed);
        csBlue = new ColourScript("Blue", colourBlue);
        csGreen = new ColourScript("Green", colourGreen);
        csPink = new ColourScript("Pink", colourPink);
        //we can also use the colourscript class to store the next combination to be shown.
        //they are initialized here
        csNext = new ColourScript("null", Color.black);
        csLast = new ColourScript("Last", Color.black);
        //Put our colours into a list to randomly select from
        colourList = new List<ColourScript>
        {
            csRed,
            csBlue, 
            csGreen, 
            csPink,
        };
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        //Reset game
        timer = 0;
        numAnswersGiven = 0;
        gameIsRunning = true;
        //Gets first word
        getNextColourWord();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsRunning)
        {
            timer += Time.deltaTime;
            timerText.text = timer.ToString("F2");
        }

        //Added hotkeys for quick play
        if (Input.GetKeyDown(KeyCode.Q))
            redButtonClick();
        if (Input.GetKeyDown(KeyCode.W))
            blueButtonClick();
        if (Input.GetKeyDown(KeyCode.E))
            greenButtonClick();
        if (Input.GetKeyDown(KeyCode.R))
            pinkButtonClick();
    }

    public void getNextColourWord()
    {
        string nextWord = "";
        Color nextColour = Color.black;

        //Sets values of last word used to compare with new one
        csLast.word = csNext.word;
        csLast.colour = csNext.colour;

        //will look for a new combination if their is a repeat
        while (csNext.IsEqualTo(csLast))
        {
            nextWord = getNextWord();
            nextColour = getNextColour(nextWord);
            //assign our next colour object its variables
            csNext.word = nextWord;
            csNext.colour = nextColour;
        }

        //set the text to equal our new combination
        SetColourText();
    }

    private void SetColourText()
    {
        colourText.text = csNext.word;
        colourText.color = csNext.colour;
    }

    public string getNextWord()
    {
        //get a random number to select our list item with
        int i = Random.Range(0, (colourList.Count));
        //get the colour text from that list item
        string selectedWord = colourList[i].word;
        return selectedWord;
    }

    public Color getNextColour(string selectedWord)
    {
        //a string to test if the new colour we are selecting matches the word
        string selectedWordCompare = selectedWord;
        //random list index;
        int index = -1;
        
        //if the colour matches the word, we try again
        while (selectedWord == selectedWordCompare)
        {
            //get a random number to select our list item with
            index = Random.Range(0, (colourList.Count));
            selectedWordCompare = colourList[index].word;
        }
        //get the colour from that list item
        Color selectedColour = colourList[index].colour;
        return selectedColour;
    }

    private void pinkButtonClick()
    {
        CheckPlayerAnswer(colourPink);
    }

    private void greenButtonClick()
    {
        CheckPlayerAnswer(colourGreen);
    }

    private void blueButtonClick()
    {
        CheckPlayerAnswer(colourBlue);
    }

    private void redButtonClick()
    {
        CheckPlayerAnswer(colourRed);
    }


    private void CheckPlayerAnswer(Color color)
    {            
        //if player answers correctly add to answers given count, check if all questions are answered yet. IF not gets next word.
        if (color == csNext.colour)
        {
            numAnswersGiven++;
            if (!HasPlayerWon(numAnswersGiven))
                getNextColourWord();
        }
        //IF answer is incorrect game ends.
        else
        {
            EndGame(false);
        }
    }

    private bool HasPlayerWon(int answers)
    {
        //if player has answered all correctly
        if (answers == numOfQuestions)
        {
            EndGame(true);
            return true;
        }
        else
            return false;
    }
    //calls our navigation script to take us to end game screen.
    private void EndGame(bool didPlayerWin)
    {
        //pause timer on game end
        gameIsRunning = false;
        //go to result screen with results
        navigationScript.ShowResultScreen(didPlayerWin, timer);
    }
}
