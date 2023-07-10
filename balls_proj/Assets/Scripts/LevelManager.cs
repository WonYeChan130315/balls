using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Ball ball;
    public float levelScore;

    private float level = 1;

    private void Update() {
        if(ball.score >= level * levelScore && level < 10) {
            level++;
            ball.maxBalls++;
            ball.speed += 0.4f;
        }
    }
}
