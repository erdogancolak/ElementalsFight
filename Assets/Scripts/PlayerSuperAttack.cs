using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSuperAttack : MonoBehaviour
{
    ControlPlayer controls;
    Animator animator;
    public GameObject readyText;
    public Transform superAttackPoint;
    public float superAttackRange;
    public LayerMask enemyLayer;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        controls = new ControlPlayer();
        controls.Gameplay.Enable();
        controls.Gameplay.SuperAttack.performed += SuperAttackInput;

    }

    private void SuperAttackInput(InputAction.CallbackContext context)
    {
        if(EnergyBar.Energy >= 400)
        {
            if(context.ReadValueAsButton())
            {
                readyText.SetActive(false);
                EnergyBar.Energy = 0;
                animator.SetTrigger("SuperAttack");
                SuperAttack();
            }
        }
    }

    void SuperAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(superAttackPoint.position, superAttackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(250);
        }
    }
}
