using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator =  GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.linearVelocity = speed * horizontal * Vector2.right;
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
        } else if (horizontal > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
    }
}
