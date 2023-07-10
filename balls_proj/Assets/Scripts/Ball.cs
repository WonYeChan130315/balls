using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector2[] pos;
    public GameObject[] ballPrefabs;
    public int maxBalls;
    public float speed;
    public float score;
    public float unnecessary;

    private Vector2 lastVelocity;
    private Rigidbody2D rb;
    private int currentBalls;
    private bool click;
    private bool noClick;
    private bool onTrigger;
    private float triggerTime;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        RandomBounce(rb);
    }

    private void Update() {
        lastVelocity = rb.velocity;
        if(Input.GetMouseButtonDown(0) && !onTrigger) {
            unnecessary++;
        }
        if(rb.velocity == Vector2.zero) {
            RandomBounce(rb);
        }

        if(onTrigger) {
            triggerTime += Time.deltaTime;
        }
        if(triggerTime > 2f) {
            RandomBounce(rb);
            print("over stay");
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
        triggerTime = 0;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("CheckBox")) {
            if(tag == "Ball") {
                if(click && !noClick) {
                    Bounce(other);
                    AddBall();
                    NoClick();
                    score++;
                } else if(!click && noClick) {
                    Bounce(other);
                    AddBall();
                    NoClick();
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

    private void RandomBounce(Rigidbody2D target) {
        target.velocity = pos[Random.Range(0, pos.Length)].normalized * speed;
    }

    private void AddBall() {
        if(Random.Range(0, 3) == 0 && maxBalls > currentBalls) {
            Instantiate(ballPrefabs[Random.Range(0, ballPrefabs.Length)]);
            currentBalls++;
        }
    }

    private void NoClick() {
        if(Random.Range(0, 6) == 0) {
            noClick = true;
            GetComponent<SpriteRenderer>().color = Color.white;
        } else {
            noClick = false;
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    private void Gameover() {
        print("gameover");
        Time.timeScale = 0;
    }
}