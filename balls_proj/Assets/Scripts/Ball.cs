using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject linePrefab;
    public float speed;

    private Vector2 lastVelocity;
    private Rigidbody2D rigidbody;
    private bool click;
    private bool collision;
    private bool enterClick;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();

        Vector2 ramdomVec = Random.insideUnitSphere;
        rigidbody.AddForce(ramdomVec.normalized * speed);
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
                if(click) {
                    Bounce(other);
                    click = false;
                } else {
                    print("game over!");
                    Time.timeScale = 0;
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

    private void Bounce(Collision2D other) {
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, other.contacts[0].normal);

        rigidbody.velocity = direction * speed;
    }
}