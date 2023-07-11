using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Ball ball;
    public Text scoreTxt;
    public Text bestScoreTxt;
    public GameObject ballobj;
    public GameObject startGroup;
    public GameObject gamingGroup;

    private int bestScore;

    private void Awake() {
        bestScore = PlayerPrefs.GetInt("bestScore");
    }

    private void Update() {
        scoreTxt.text = string.Format("{0:D4}{1}", ball.score, ball.problemClick > 0 ? "-" + ball.problemClick.ToString() : "");
        bestScoreTxt.text = string.Format("{0:D4}", bestScore);

        if(ball.score > bestScore) {
            bestScore = ball.score;
            PlayerPrefs.SetInt("bestScore", bestScore);
        }
    }

    public void Reset() {
        Time.timeScale = 0;
        startGroup.SetActive(true);
    }

    public void GameStart() {
        ball.transform.position = Vector3.zero;
        ball.RandomBounce(ball.GetComponent<Rigidbody2D>());

        ballobj.SetActive(true);
        startGroup.SetActive(false);
        gamingGroup.SetActive(true);
        Time.timeScale = 1;
    }
}
