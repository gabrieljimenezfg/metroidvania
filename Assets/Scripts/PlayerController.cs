using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Animator animator;
    private int jumpCount;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundDistance = 0.5f;

    // Temp
    private int maxJumps = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(speed * horizontal, rb.linearVelocity.y);

        if (horizontal == 0f)
        {
            animator.SetBool("IsRunning", false);
        }
        else
        {
            animator.SetBool("IsRunning", true);
        }

        if (horizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (horizontal > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            jumpCount++;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, groundDistance);
        bool isGrounded = false;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform.tag == "Ground")
            {
                isGrounded = true;
                break;
            }
        }

        if (isGrounded)
        {
            jumpCount = 0;
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsJumping", true);
            if (jumpCount == 0)
            {
                jumpCount++;
            }
        }
    }
}