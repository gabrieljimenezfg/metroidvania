using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float life;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private bool canWalk = true;

    private float currentAttackingCooldownTimer;
    [SerializeField] private float attackingCooldown;

    public bool playerDetected;
    private Rigidbody2D rb;
    private Transform player;
    public float stopDistance;
    private bool isAttacking;
    private bool isDead;

    public event EventHandler Alerted;
    public event EventHandler<bool> IsAttackingChanged;
    public event EventHandler TookDamage;
    public event EventHandler Died;
    public event EventHandler<bool> PlayerDetectedChanged;
    public event EventHandler<bool> RestingChanged;


    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentAttackingCooldownTimer = attackingCooldown;
    }

    private Vector2 GetVector2WithVerticalForce(float x)
    {
        return new Vector2(x, rb.linearVelocity.y);
    }
    

    protected void Update()
    {
        if (isDead || !playerDetected) return;
        Vector3 direction = player.position - transform.position;
        HandleRotation(direction);

        currentAttackingCooldownTimer += Time.deltaTime;

        if (isAttacking || currentAttackingCooldownTimer < attackingCooldown)
        {
            rb.linearVelocity = GetVector2WithVerticalForce(0);
            return;
        }

        HandleMovement(direction);
        HandleAttack(direction);
    }

    private void HandleAttack(Vector3 direction)
    {
        if (currentAttackingCooldownTimer < attackingCooldown) return;
        RestingChanged?.Invoke(this, false);

        var isInRange = CheckIfPlayerInAttackRange(direction);

        if (!isInRange) return;
        
        rb.linearVelocity = GetVector2WithVerticalForce(0);
        SetIsAttacking(true);
    }

    private bool CheckIfPlayerInAttackRange(Vector3 direction)
    {
        float distanceSquared = direction.sqrMagnitude;
        return distanceSquared <= Mathf.Pow(stopDistance, 2);
    }

    private void HandleRotation(Vector3 direction)
    {
        if (direction.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (direction.x < 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    private void HandleMovement(Vector3 direction)
    {
        if (!canWalk) return;

        if (direction.x > 0)
        {
            rb.linearVelocity = GetVector2WithVerticalForce(speed);
        }
        else if (direction.x < 0)
        {
            rb.linearVelocity = GetVector2WithVerticalForce(-speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (playerDetected) return;

        player = other.transform;
        Alerted?.Invoke(this, EventArgs.Empty);
    }

    private void SetIsAttacking(bool isNowAttacking)
    {
        isAttacking = isNowAttacking;
        IsAttackingChanged?.Invoke(this, isNowAttacking);
    }

    private void SetPlayerDetected(bool isDetected)
    {
        playerDetected = isDetected;
        PlayerDetectedChanged?.Invoke(this, playerDetected);
    }

    public void StartFollowing()
    {
        SetPlayerDetected(true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }

    public void TakeDamage(float damageTaken)
    {
        life -= damageTaken;
        if (life <= 0)
        {
            rb.simulated = false;
            GetComponent<Collider2D>().enabled = false;
            isDead = true;
            Died?.Invoke(this, EventArgs.Empty);
            DestroyAfterDelay();
        }
        else
        {
            TookDamage?.Invoke(this, EventArgs.Empty);
        }
    }

    private void ResetAttackingCooldown()
    {
        currentAttackingCooldownTimer = 0f;
        RestingChanged?.Invoke(this, true);
    }

    public void FinishAttack()
    {
        ResetAttackingCooldown();
        SetIsAttacking(false);
    }

    private void DestroyAfterDelay()
    {
        const float delayBeforeDeath = 6f;
        Destroy(gameObject, delayBeforeDeath);
    }

    public virtual void OnProjectileLaunch()
    {
    }
}