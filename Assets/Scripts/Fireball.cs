using System;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (speed == 0) return;
        rb.linearVelocity = speed * transform.right;
    }

    private void StartDeathAnimation()
    {
        speed = 0;
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("Death");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player") return;

        StartDeathAnimation();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}