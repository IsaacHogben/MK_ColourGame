using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreScript : MonoBehaviour
{
    private float high_Score;

    //can get highscore
    public float highScore { get => high_Score;}

    //Checks the new score against the old
    public bool IsNewHighScore(float score)
    {
        if (score > high_Score)
        {
            high_Score = score;
            return true;
        }
        else
            return false;
    }
}
