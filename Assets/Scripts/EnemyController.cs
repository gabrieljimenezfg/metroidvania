using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static readonly int PlayerDetected = Animator.StringToHash("PlayerDetected");

    [SerializeField] private float life;
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    public bool playerDetected;
    private Rigidbody2D rb;
    private Transform player;
    public float stopDistance;
    public bool isAttacking;
    private bool isDead;

    public event EventHandler Alerted;
    public event EventHandler<bool> IsAttackingChanged;
    public event EventHandler TookDamage;
    public event EventHandler Died;
    public event EventHandler<bool> PlayerDetectedChanged;


    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private Vector2 GetVector2WithVerticalForce(float x)
    {
        return new Vector2(x, rb.linearVelocity.y);
    }

    protected void Update()
    {
        if (isDead || !playerDetected) return;
        if (isAttacking)
        {
            rb.linearVelocity = GetVector2WithVerticalForce(0);
            return;
        }

        Vector3 direction = player.position - transform.position;
        Debug.Log("direction " + direction);
        Debug.Log("linear velocity " + rb.linearVelocity);
        HandleMovement(direction);
        CheckIfPlayerInAttackRange(direction);
    }

    private void CheckIfPlayerInAttackRange(Vector3 direction)
    {
        float distanceSquared = direction.sqrMagnitude;
        if (distanceSquared <= Mathf.Pow(stopDistance, 2))
        {
            rb.linearVelocity = GetVector2WithVerticalForce(0);
            SetIsAttacking(true);
        }
    }

    private void HandleMovement(Vector3 direction)
    {
        if (direction.x > 0)
        {
            rb.linearVelocity = GetVector2WithVerticalForce(speed);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (direction.x < 0)
        {
            rb.linearVelocity = GetVector2WithVerticalForce(-speed);
            transform.eulerAngles = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        player = other.transform;
        Alerted?.Invoke(this, EventArgs.Empty);
    }

    protected void SetIsAttacking(bool isNowAttacking)
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

    protected bool CheckIfIsInStopDistanceRange()
    {
        var direction = player.position - transform.position;
        var distanceSquared = direction.sqrMagnitude;
        return distanceSquared <= Mathf.Pow(stopDistance, 2);
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

    public void FinishAttack()
    {
        var isInStopRange = CheckIfIsInStopDistanceRange();
        if (!isInStopRange)
        {
            SetIsAttacking(false);
        }
    }

    private void DestroyAfterDelay()
    {
        const float delayBeforeDeath = 6f;
        Destroy(this, delayBeforeDeath);
    }

    public virtual void OnProjectileLaunch()
    {
    }
}