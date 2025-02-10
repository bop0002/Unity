using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int playerscore;
    private int computerscore;
    public Ball ball;

    public TMPro.TMP_Text PlayerText;
    public TMPro.TMP_Text ComputerText;

    public void PlayerScores()
    {
        playerscore++;
        PlayerText.text = playerscore.ToString();
        this.ball.GameReset();
    }
    public void ComputerScores()
    {
        computerscore++;
        ComputerText.text = computerscore.ToString();
        this.ball.GameReset();
    }


}
