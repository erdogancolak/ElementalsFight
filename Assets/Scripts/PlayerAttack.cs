using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerAttack : MonoBehaviour
{
    ControlPlayer controls;
    Animator animator;

    private int comboIndex = 0;
    private float comboTimer;
    public float comboResetTime;

    public Transform attackPoint;
    public Transform windPoint,jumpPoint;
    public float attackRange;
    public LayerMask enemyLayer;
    public float attackRate;
    float nextAttackTime = 0;
    bool InputCheck = false;

    private void Awake()
    {
        controls = new ControlPlayer();
        controls.Gameplay.Enable();
        controls.Gameplay.Attack.performed +=  AttackInput;
    }

    private void AttackInput(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && !PlayerHealth.DefendCheck)
        {
            if (comboIndex == 0)
            {
                animator.SetTrigger("ComboCheck");
            }

            InputCheck = true;
            Attack();
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
       if(comboIndex > 0)
        {
            comboTimer += Time.deltaTime;
            if(comboTimer > comboResetTime)
            {
                ResetCombo();
            }
        }
    }

    void Attack()
    {
        PerformCombo();
        if (Time.time >= nextAttackTime && InputCheck)
        {
            InputCheck = false;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyController>().TakeDamage(50);
                EnergyBar.Energy += 30;
            }
            nextAttackTime = Time.time + 1 / attackRate;
        }
    }

    void PerformCombo()
    {
        GetComponent<PlayerMovement>().enabled = false;
        comboTimer = 0f;
        if(comboIndex < 3)
        {
             comboIndex++;
            if(comboIndex == 1)
            {
                attackRange = 1.1f;
            }
            if (comboIndex == 2)
            {
                attackRange = 1.7f;               
            }
            if(comboIndex == 3)
            {
                windPoint.GetComponent<Collider2D>().enabled = true;
                windPoint.DOMove(jumpPoint.position, 1).OnComplete(() =>
                {
                    windPoint.position = attackPoint.position;
                    windPoint.GetComponent<Collider2D>().enabled = false;
                });
            }
            animator.SetInteger("comboIndex", comboIndex);
            animator.SetBool("isAttacking", true);
        }
    }

    void ResetCombo()
    {
        GetComponent<PlayerMovement>().enabled = true;
        comboIndex = 0;
        animator.SetInteger("comboIndex", comboIndex);
        animator.SetBool("isAttacking", false);
    }
}
