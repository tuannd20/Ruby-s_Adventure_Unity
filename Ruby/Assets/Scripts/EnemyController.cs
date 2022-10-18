using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigidbody2D;
    public float changeTime = 3.0f;
    public bool vertical;

    float timer;
    int direction = 1;
    Animator animator;

    void start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    void update() {
        timer -= Time.deltaTime;

        if (timer < 0) {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate() {
        Vector2 position = rigidbody2D.position;

        if (vertical) {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        
        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D  other) {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null) {
            player.ChangeHealth(-1);
        }
    }
}
