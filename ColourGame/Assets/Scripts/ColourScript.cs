using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourScript : MonoBehaviour
{
    //A class created to hold the colour varialbe and the name
    //this is done so the displayed text and text color wont be the same
    private string name_String;
    private Color colour_;

    public string word { get => name_String; set => name_String = value; }
    public Color colour { get => colour_; set => colour_ = value; }

    public ColourScript(string name, Color colour)
    {
        this.name_String = name;
        this.colour_ = colour;
    }
    //no-arg constructor
    public ColourScript()
    {
        this.name_String = "none";
        this.colour_ = Color.black;
    }

    //a comparator to asses if two combinations are the same
    public bool IsEqualTo(ColourScript other)
    {
        if ((this.name_String == other.word) && (this.colour == other.colour))
            return true;       
        else
            return false;
    }
}
