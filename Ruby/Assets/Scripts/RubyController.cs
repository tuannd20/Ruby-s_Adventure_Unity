using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    public float speed = 3.0f;
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    public int health { get { return currentHealth; }}

    int currentHealth;

    bool isInvincible;
    float invincibleTimer;

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        // Debug.Log(horizontal);
        // Vector2 position = transform.position;
        // position.x = position.x + 3.0f * horizontal * Time.deltaTime;
        // position.y = position.y + 3.0f * vertical * Time.deltaTime;
        // transform.position = position;

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible) {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            RaycastHit2D hit = Physics2D.Raycast(
                rigidbody2d.position + Vector2.up * 0.2f, 
                lookDirection, 
                1.0f, 
                LayerMask.GetMask("NPC")
            );

            if (hit.collider != null) {
                Debug.Log("Raycast has hit the object " + hit.collider.gameObject);

                NonPlayerCharacter playerCharacter = hit.collider.GetComponent<NonPlayerCharacter>();
                if (playerCharacter != null) {
                    playerCharacter.DisplayDialog();
                }
            }
        }
    }

    void FixedUpdate() {
        Vector2 position = rigidbody2d.position;;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount) {
        if (amount < 0) {
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);

        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Launch() {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300.0f);

        animator.SetTrigger("Launch");
    }
}
