using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementNew : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float JumpHeight = 16f;
    private Rigidbody2D rb;
    private float horizontal;
    private bool isFacingRight = true;
    private bool isGrounded;
    public Animator Anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Movement();//fungsi Berjalan
        Jump();//fungsi loncat
        Flip();//fungsi Berputar
        AnimateMove(); // fungsi animasi berjalan

    }

    private void Movement()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    void AnimateMove()
    {
        if (horizontal >= 0.1f || horizontal <= -0.1f)
        {
            Anim.SetBool("isRunning", true);
        }
        else
        {
            Anim.SetBool("isRunning", false);
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Vector2 direction = new Vector2(0, 1);
            rb.velocity = direction * JumpHeight;
        }

    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
 
            isGrounded = true;
            Debug.Log("isGrounded");
            Anim.SetBool("isJumping", false);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("no");
            Anim.SetBool("isJumping", true);
        }
    }

}

