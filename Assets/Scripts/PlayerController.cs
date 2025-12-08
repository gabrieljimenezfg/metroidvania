using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int DamageTaken = Animator.StringToHash("DamageTaken");
    private static readonly int PlayerDied = Animator.StringToHash("PlayerDied");
    private static readonly int IsKnocked = Animator.StringToHash("IsKnocked");

    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Animator animator;
    private int jumpCount;
    private int comboCount;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float fireballManaCost;

    [SerializeField] private float normalGravityScale;
    [SerializeField] private float fallingGravityScale;

    [SerializeField] private float knockBackTime;

    [SerializeField] private float jumpForce;
    [SerializeField] private float groundDistance = 0.5f;
    [SerializeField] private float fireballCooldown = 0.5f;
    
    [SerializeField] private float dashStrength;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown = 1f;
    private float dashCooldownTimer;
    private bool airDashed;

    private bool isGrounded;
    private float fireballTimer;
    private bool isDashing;

    public event EventHandler TookDamage;
    public event EventHandler UsedMana;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        fireballTimer = fireballCooldown;
        dashCooldownTimer = dashCooldown;
        rb.gravityScale = normalGravityScale;
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

        if (Input.GetButtonDown("Jump") && jumpCount < GameManager.Instance.GameDataObject.PlayerMaxJumps)
        {
            jumpCount++;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void CheckFireball()
    {
        if (Input.GetButtonDown("FireBall"))
        {
            if (fireballTimer >= fireballCooldown &&
                GameManager.Instance.GameDataObject.PlayerCurrentMana >= fireballManaCost)
            {
                UsedMana?.Invoke(this, EventArgs.Empty);
                GameManager.Instance.GameDataObject.PlayerCurrentMana -= fireballManaCost;
                Instantiate(fireballPrefab, spawnPoint.position, spawnPoint.rotation);
                fireballTimer = 0;
            }
        }
    }

    private void HandleDashing()
    {
        // if (!GameManager.Instance.GameDataObject.HasDash) return;
        if (!Input.GetButtonDown("Dash") || isDashing) return;
        if (dashCooldownTimer < dashCooldown) return;
        if (!isGrounded && airDashed) return;

        isDashing = true;
        dashCooldownTimer = 0f;

        var direction = transform.eulerAngles == Vector3.zero ? 1 : -1;

        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(dashStrength * direction, 0);

        if (!isGrounded)
        {
            airDashed = true;
        }

        StartCoroutine(EndDashCoroutine());
    }

    private IEnumerator EndDashCoroutine()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        rb.gravityScale = normalGravityScale;
    }

    void Update()
    {
        if (animator.GetBool(IsKnocked)) return;

        HandleDashing();
        ModifyGravityScale();

        fireballTimer += Time.deltaTime;
        dashCooldownTimer += Time.deltaTime;
        
        if (comboCount == 0 && !isDashing)
        {
            CheckMovement();
            CheckJump();
            CheckFireball();
        }
        else if (comboCount != 0)
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

    private void ModifyGravityScale()
    {
        if (isDashing) return;
        
        if (!isGrounded && rb.linearVelocity.y < 0.2f)
        {
            rb.gravityScale = fallingGravityScale;
        }
        else
        {
            rb.gravityScale = normalGravityScale;
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
        isGrounded = false;

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
            airDashed = false;
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
            var damageToHit = animatorComboCount > 0
                ? GameManager.Instance.GameDataObject.PlayerDamage
                : GameManager.Instance.GameDataObject.HeavyDamage;

            try
            {
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(damageToHit);
            }
            catch
            {
                collision.gameObject.GetComponent<BossController>().TakeDamage(damageToHit);
            }
        }
    }

    public void TakeDamage(float damageTaken)
    {
        GameManager.Instance.GameDataObject.PlayerCurrentLife -= damageTaken;
        TookDamage?.Invoke(this, EventArgs.Empty);

        if (GameManager.Instance.GameDataObject.PlayerCurrentLife <= 0)
        {
            animator.SetTrigger(PlayerDied);
        }
        else
        {
            animator.SetTrigger(DamageTaken);
        }
    }

    public void GetKnockedBack(Vector3 force)
    {
        rb.AddForce(force);
    }

    public IEnumerator KnockBackCoroutine()
    {
        animator.SetBool(IsKnocked, true);
        yield return new WaitForSeconds(knockBackTime);
        rb.linearVelocity = Vector2.zero;
        animator.SetBool(IsKnocked, false);
    }
}