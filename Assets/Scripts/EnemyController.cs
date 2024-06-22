using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    public int maxHealth;
    int currentHealth;
    public float jumpPower;
    public static EnemyController instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }
    void Update()
    {
        
    }

    public void TakeDamage(int Damage)
    {
        currentHealth -= Damage;
        animator.SetTrigger("Hit");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("isDead",true);
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
        if(rb.velocity.y <= 0)
        {
            animator.SetBool("isFall", true);
        }
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.collider.CompareTag("Ground"))
        {
            animator.SetBool("isFall", false);
        }
    }
}
