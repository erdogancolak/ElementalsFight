using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    ControlPlayer controls;
    Rigidbody2D rb;
    Animator animator;

    public float runSpeed;
    public float jumpPower;
    float sideMove;
    bool isGrounded = true;
    bool InputCheck = false;

    private void Awake()
    {
        controls = new ControlPlayer();
        controls.Gameplay.Enable();
        controls.Gameplay.Jump.performed += JumpInput;
    }

    private void JumpInput(InputAction.CallbackContext context)
    {
        if(context.ReadValueAsButton() && !PlayerHealth.DefendCheck )
        {
            InputCheck = true;
            Jump();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        Move();
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetBool("isFall", false);
        if(collision.collider.CompareTag("Ground"))
        {
            StartCoroutine(waitJump());
        }
    }
    void Move()
    {
        sideMove = Input.GetAxis("Horizontal");
        Vector2 newScale = transform.localScale;
        if (sideMove > 0)
        {
            animator.SetBool("isRun", true);
            newScale.x = 5f;
        }
        if (sideMove == 0)
        {
            animator.SetBool("isRun", false);
        }
        if (sideMove < 0)
        {
            animator.SetBool("isRun", true);
            newScale.x = -5f;
        }
        transform.localScale = newScale;
        rb.velocity = new Vector2(sideMove * runSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        if ( InputCheck && isGrounded)
        {
            isGrounded = false;
            InputCheck = false;
            animator.SetTrigger("Jump");
            if (rb.velocity.y <= 0)
            {
                animator.SetBool("isFall", true);
            }
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

    IEnumerator waitJump()
    {
        yield return new WaitForSeconds(.2f);
        isGrounded = true;
    }
}