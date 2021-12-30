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
    //The float used to track time
    private float timer;
    //Colours
    public Color colourRed = new Color(255f,255f,255f);
    private Color colourBlue = new Color(0, 0, 255f);
    private Color colourGreen = new Color(0, 255f, 0);
    private Color colourPink = new Color(255f, 51f, 255f);
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

    // Start is called before the first frame update
    void Start()
    {
        //initialize colour objects
        csRed = new ColourScript("Red", colourRed);
        csBlue = new ColourScript("Blue", colourBlue);
        csGreen = new ColourScript("Green", colourGreen);
        csPink = new ColourScript("Pink", colourPink);
        //we can also use the colourscript class to store the next combination to be shown.
        //they are initialized here
        csNext = new ColourScript();
        csLast = new ColourScript();
        //Put our colours into a list to randomly select from
        colourList = new List<ColourScript>
        {
            csRed,
            csBlue, 
            csGreen, 
            csPink,
        };

        timer = 0; //reset timer

        getNextColourWord();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("F2");
    }

    public void getNextColourWord()
    {
        string nextWord;
        Color nextColour;
        nextWord = getNextWord();
        nextColour = getNextColour(nextWord);
        //assign our next colour object its variables
        csNext.word = nextWord;
        csNext.colour = nextColour;
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
}
