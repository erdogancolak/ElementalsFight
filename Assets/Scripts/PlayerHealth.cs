using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerHealth : MonoBehaviour
{
    ControlPlayer controls;
    Animator animator;

    public int maxHealth;
    int currentHealth;
    bool canTakeDamage = true;
    public static bool DefendCheck = false;

    private void Awake()
    {
        controls = new ControlPlayer();
        controls.Gameplay.Enable();
        controls.Gameplay.Defend.performed += ctx => Defend();
        controls.Gameplay.Defend.canceled += ctx => unDefend();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    void Defend()
    {
        DefendCheck = true;
        animator.SetTrigger("Defend");
        animator.SetBool("isDefend" , true);
        //GetComponent<PlayerAttack>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        canTakeDamage = false;
    }

    void unDefend()
    {
        DefendCheck = false;
        animator.SetBool("isDefend", false);
        canTakeDamage = true;
        //GetComponent<PlayerAttack>().enabled = true;
        GetComponent<PlayerMovement>().enabled = true;
    }

    public void TakeDamage(int Damage)
    {
        if(canTakeDamage)
        {
            currentHealth -= Damage;
            animator.SetTrigger("Hit");
            if(currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        animator.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
