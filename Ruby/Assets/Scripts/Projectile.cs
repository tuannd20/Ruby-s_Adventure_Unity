using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 100.0f) {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force) {
        // rigidbody2d = GetComponent<Rigidbody2D>();

        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other) {
        // Debug.Log("Test" + other.gameObject);
        EnemyController2 enemyBot = other.collider.GetComponent<EnemyController2>();
        if (enemyBot != null) {
            enemyBot.Fix();
        }

        Destroy(gameObject);
    }
}
