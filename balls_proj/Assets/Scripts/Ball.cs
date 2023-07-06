using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject[] ballPrefabs;
    public float speed;

    private Vector2 lastVelocity;
    private Rigidbody2D rigidbody;
    private bool click;
    private bool noClick;
    private bool collision;
    private bool enterClick;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        RandomBounce(transform);
    }

    private void Update() {
        lastVelocity = rigidbody.velocity;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(!collision && Input.GetMouseButton(0)) {
            enterClick = true;
        }

        if(!collision && enterClick && Input.GetMouseButtonUp(0)) {
            enterClick = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Wall")) {
            if(tag == "NotBall") return;
            
            collision = true;
            if(Input.GetMouseButton(0) && !enterClick) {
                print("click");
                click = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("CheckBox")) {
            if(tag == "Ball") {
                if(click && !noClick) {
                    if(Random.Range(0, 6) == 0) AddBall();

                    if(Random.Range(0, 11) == 0) {
                        noClick = true;
                        GetComponent<SpriteRenderer>().color = Color.white;
                    } else {
                        noClick = false;
                        GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                    
                    Bounce(other);
                    click = false;
                } else if(!click && noClick) {
                    Bounce(other);
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

    private void OnTriggerExit2D(Collider2D other) {
        if(tag == "NotBall") return;
        collision = false;
    }

    private void Gameover() {
        print("game over!");
        Time.timeScale = 0;
    }

    private void RandomBounce(Transform target) {
        Vector2 ramdomVec = Random.insideUnitSphere;
        target.GetComponent<Rigidbody2D>().AddForce(ramdomVec.normalized * speed);
    }

    private void Bounce(Collision2D other) {
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, other.contacts[0].normal);

        rigidbody.velocity = direction * speed;
    }

    private void AddBall() {
        GameObject ball = Instantiate(ballPrefabs[Random.Range(0, ballPrefabs.Length)]);
        RandomBounce(ball.transform);
    }
}