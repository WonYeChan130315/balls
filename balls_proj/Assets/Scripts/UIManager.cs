using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Ball ball;
    public Text scoreTxt;
    public Text bestScoreTxt;

    private int bestScore;

    private void Update() {
        string result = string.Format("{0:0000}{1}", ball.score, ball.unnecessary > 0 ? "-" + ball.unnecessary.ToString() : "");
        scoreTxt.text = result;

        if(ball.score > bestScore) {
            bestScore = ball.score;
            bestScoreTxt.text = string.Format("{0:0000}", bestScore);
        }
    }
}
