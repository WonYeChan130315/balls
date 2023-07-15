using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Ball ball;
    public Text scoreTxt;
    public Text bestScoreTxt;
    public GameObject player;
    public GameObject ballobj;
    public GameObject startGroup;
    public GameObject gamingGroup;

    private int bestScore;

    private void Awake() {
        bestScore = PlayerPrefs.GetInt("bestScore");
        player.GetComponent<Rigidbody2D>().simulated = false;
    }

    private void Update() {
        scoreTxt.text = string.Format("{0:D4}{1}", ball.score, ball.problemClick > 0 ? "-" + ball.problemClick.ToString() : "");
        bestScoreTxt.text = string.Format("{0:D4}", bestScore);

        if(ball.score - ball.problemClick > bestScore) {
            bestScore = ball.score;
            PlayerPrefs.SetInt("bestScore", bestScore);
        }
    }

    public void Reset() {
        player.GetComponent<Rigidbody2D>().simulated = false;
        player.transform.position = Vector3.zero;
        GameObject[] notBalls = GameObject.FindGameObjectsWithTag("NotBall");
        for(int i = 0; i < notBalls.Length; i++) {
            Destroy(notBalls[i]);
        }
        startGroup.SetActive(true);
        gamingGroup.SetActive(false);
    }

    public void GameStart() {
        ball.transform.position = Vector3.zero;
        ball.RandomBounce(ball.GetComponent<Rigidbody2D>());
        ball.score = 0;
        ball.problemClick = 0;

        ballobj.SetActive(true);
        startGroup.SetActive(false);
        gamingGroup.SetActive(true);
        player.GetComponent<Rigidbody2D>().simulated = true;
    }
}
