using UnityEngine;

public class Ball : MonoBehaviour
{
    public Color color;
    public Vector2[] pos;
    public GameObject ballPrefab;
    public int score;
    public int maxBalls;
    public int problemClick;
    public float speed;

    private Vector2 lastVelocity;
    private Rigidbody2D rb;
    private int currentBalls;
    private bool click;
    private bool noClick;
    private bool onTrigger;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        RandomBounce(rb);
    }

    private void Update() {
        lastVelocity = rb.velocity;
        if(Input.GetMouseButtonDown(0) && !onTrigger) {
            problemClick++;
        }

        if(rb.velocity == Vector2.zero) {
            RandomBounce(rb);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(tag == "NotBall") return;

        if(other.CompareTag("Wall")) {
            onTrigger = true;
            if(Input.GetMouseButton(0)) {
                click = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(tag == "NotBall") return;
        
        onTrigger = false;
        click = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("CheckBox")) {
            if(tag == "Ball") {
                if((click && !noClick) || (!click && noClick)) {
                    Bounce(other);
                    NoClick();
                    AddBall();
                    score++;
                } else {
                    Gameover();
                }
            } else {
                Bounce(other);
            }
        } else {
            Bounce(other);
        }
    }

    private void Bounce(Collision2D other) {
        Vector2 direction = Vector3.Reflect(lastVelocity.normalized, other.contacts[0].normal);
        rb.velocity = direction * speed;
    }

    public void RandomBounce(Rigidbody2D target) {
        target.velocity = pos[Random.Range(0, pos.Length)].normalized * speed;
    }

    private void AddBall() {
        if(Random.Range(0, 3) == 0 && maxBalls > currentBalls) {
            Instantiate(ballPrefab);
            currentBalls++;
        }
    }

    private void NoClick() {
        if(Random.Range(0, 5) == 0) {
            noClick = true;
            GetComponent<SpriteRenderer>().color = color;
        } else {
            noClick = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void Gameover() {
        Invoke("Reset", 1f);
        GameObject[] notBalls = GameObject.FindGameObjectsWithTag("NotBall");
        for(int i = 0; i < notBalls.Length; i++) {
            notBalls[i].GetComponent<Rigidbody2D>().simulated = false;
        }
        rb.simulated = false;
    }

    private void Reset() {
        GameObject.Find("UIManager").GetComponent<UIManager>().Reset();
    }
}