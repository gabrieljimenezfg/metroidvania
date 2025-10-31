using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int DamageTaken = Animator.StringToHash("DamageTaken");
    private static readonly int PlayerDied = Animator.StringToHash("PlayerDied");

    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Animator animator;
    private int jumpCount;
    private int comboCount;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float fireballManaCost;

    [SerializeField] private float jumpForce;
    [SerializeField] private float groundDistance = 0.5f;
    [SerializeField] private float fireballCooldown = 0.5f;
    private float fireballTimer;

    // Temp
    private int maxJumps = 1;
    public float mana;
    public float maxMana;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void CheckMovement()
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
    }

    private void CheckFireball()
    {
        if (Input.GetButtonDown("FireBall"))
        {
            if (fireballTimer >= fireballCooldown && mana >= fireballManaCost)
            {
                mana -= fireballManaCost;
                Instantiate(fireballPrefab, spawnPoint.position, spawnPoint.rotation);
                fireballTimer = 0;
            }
        }
    }

    void Update()
    {
        fireballTimer += Time.deltaTime;
        if (comboCount == 0)
        {
            CheckMovement();
            CheckJump();
            CheckFireball();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (jumpCount == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SetComboCount(comboCount + 1);
            }

            if (Input.GetButtonDown("Fire2") && comboCount == 0)
            {
                animator.SetTrigger("StrongAttack");
                comboCount++;
            }
        }
    }

    private void SetComboCount(int combo)
    {
        var newComboValue = Mathf.Clamp(combo, 0, 2);
        comboCount = newComboValue;
        animator.SetInteger("Combo", newComboValue);
    }

    public void CheckCombo1()
    {
        if (comboCount < 2)
        {
            SetComboCount(0);
        }
    }

    public void CheckCombo2()
    {
        SetComboCount(0);
    }

    public void FinishHeavyAttack()
    {
        comboCount = 0;
    }

    void CheckJump()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var animatorComboCount = animator.GetInteger("Combo");
            var damageToHit = animatorComboCount > 0 ? GameManager.Instance.GameDataObject.PlayerDamage : GameManager.Instance.GameDataObject.HeavyDamage;

            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damageToHit);
        }
    }

    public void TakeDamage(float damageTaken)
    {
        GameManager.Instance.GameDataObject.PlayerCurrentLife -= damageTaken;
        if (GameManager.Instance.GameDataObject.PlayerCurrentLife <= 0)
        {
            animator.SetTrigger(PlayerDied);
            // game over panel
        }
        else
        {
            animator.SetTrigger(DamageTaken);
        }
    }
}