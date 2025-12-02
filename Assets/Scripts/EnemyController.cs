using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static readonly int PlayerDetected = Animator.StringToHash("PlayerDetected");

    [SerializeField] private float life;
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    private bool playerDetected;
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

    public void Update()
    {
        if (isDead) return;

        if (playerDetected && !isAttacking)
        {
            Vector3 direction = player.position - transform.position;
            if (direction.x > 0)
            {
                rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (direction.x < 0)
            {
                rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
                transform.eulerAngles = Vector3.zero;
            }

            float distanceSquared = direction.sqrMagnitude;
            if (distanceSquared <= Mathf.Pow(stopDistance, 2))
            {
                rb.linearVelocity = Vector2.zero;
                SetIsAttacking(true);
            }
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

    public virtual void OnProjectileLaunch()
    {
    }
}