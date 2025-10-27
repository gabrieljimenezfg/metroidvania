using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float life;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    
    private bool playerDetected;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform player;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (playerDetected)
        {
            Vector3 distance = player.position - transform.position;
            if (distance.x > 0)
            {
                rb.linearVelocity = Vector2.right * speed;        
            }
            else if (distance.x < 0)
            {
                rb.linearVelocity = Vector2.left * speed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerDetected = true;
            player = other.transform;
        }
            
    }
}