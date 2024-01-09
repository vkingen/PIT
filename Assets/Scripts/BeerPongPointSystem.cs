using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeerPongPointSystem : MonoBehaviour
{
    private int teamAScore, teamBScore;
    [SerializeField] private Text teamAScoreText, teamBScoreText;
    [SerializeField] private Text winnertext;

    public void AddPointToScore(string teamName)
    {
        if(teamName == "A")
        {
            teamAScore++;
            teamAScoreText.text = teamAScore.ToString();
            if (teamAScore == 6)
            {
                winnertext.text = "TEAM a WINS!";
            }
        }
        if (teamName == "B")
        {
            teamBScore++;
            teamBScoreText.text = teamBScore.ToString();
            if(teamBScore == 6)
            {
                winnertext.text = "TEAM B WINS!";
            }
        }

    }
}
