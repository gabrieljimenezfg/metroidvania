using System;
using System.Collections;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossController : MonoBehaviour
{
    public event EventHandler TookDamage;

    private const string JUMPING = "Jumping";
    private const string HIT_AIR = "HitAir";
    private const string HIT = "Hit";
    private const string ROLL = "Roll";
    private const string DEATH = "Death";
    private const string SPIKES = "Spikes";

    public enum BossStates
    {
        Waiting,
        Jumping,
        Roar,
        Roll,
        Spikes,
        Death
    }

    [Header("Stats")] [SerializeField] private float bossLife;
    [SerializeField] private float damage;
    [SerializeField] private float knockbackForce;

    [Header("State")] [SerializeField] private BossStates currentState;
    private Transform player;
    private Animator anim;

    [Header("Cooldown")] [SerializeField] private float waitingTime = 1f;

    [Header("Jump")] [SerializeField] private float timeToJump = 0.6f;
    [SerializeField] private float maxJump;
    [SerializeField] private float jumpSpeed;

    [Header("Roar")] [SerializeField] private float timeToSpawnBugs = 1f;
    [SerializeField] private GameObject bugMonster;
    [SerializeField] private Transform bugSpawnPoint;

    [Header("Roll")] [SerializeField] private float timeToRoll = 1.35f;
    [SerializeField] private float colliderSizeX;
    [SerializeField] private float rollSpeed = 1f;
    [SerializeField] private float outOfRollThrowForce = 300000;
    [SerializeField] private float outOfRollThrowTime = 0.2f;

    private bool collisioned;

    [Header("Spikes")] [SerializeField] private float spikesTime;
    [SerializeField] private float tiredTime;
    [SerializeField] private GameObject spikesPrefab;
    [SerializeField] private Transform[] spikesSpawnPoints;

    [Header("Death")] [SerializeField] private Sprite deathSprite;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

        currentState = BossStates.Waiting;
        ChangeState();
    }

    public void SetDeathAtStart()
    {
        Debug.Log("Death");

        GetComponent<SpriteRenderer>().sprite = deathSprite;
        anim = GetComponent<Animator>(); // Antes de desactivarlo tenemos que acceder a el. Ya que el script se encuentra desactivado
        anim.enabled = false;

        // Muerte
        GetComponent<Collider2D>().enabled = false;
        rb.gravityScale = 0;
        this.enabled = false;
    }

    void ChangeState()
    {
        switch (currentState)
        {
            case BossStates.Waiting:
                StartCoroutine(WaitingCoroutine());
                break;
            case BossStates.Jumping:
                StartCoroutine(JumpCoroutine());
                break;
            case BossStates.Roar:
                StartCoroutine(RoarCoroutine());
                break;
            case BossStates.Roll:
                StartCoroutine(RollCoroutine());
                break;
            case BossStates.Spikes:
                StartCoroutine(SpikesCoroutine());
                break;
            case BossStates.Death:
                //
                break;
        }
    }

    private void LookAtPlayer()
    {
        if (transform.position.x < player.position.x) // Derecha
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else // Izquierda
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    IEnumerator WaitingCoroutine()
    {
        LookAtPlayer();
        yield return new WaitForSeconds(waitingTime);
        LookAtPlayer();

        currentState = (BossStates)Random.Range(1, 5);
        ChangeState();
    }

    IEnumerator JumpCoroutine()
    {
        anim.SetBool(JUMPING, true);
        yield return new WaitForSeconds(timeToJump);

        Vector2 initialPointBossA = transform.position;
        float pointPlayerBX = player.position.x;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * jumpSpeed;

            float posX = Mathf.Lerp(initialPointBossA.x, pointPlayerBX, t);
            float posY = initialPointBossA.y + (4 * maxJump * t * (1 - t));

            transform.position = new Vector2(posX, posY);

            yield return null;
        }

        anim.SetBool(JUMPING, false);
        currentState = BossStates.Waiting;
        ChangeState();
    }

    IEnumerator RoarCoroutine()
    {
        anim.SetBool("Roar", true);
        yield return new WaitForSeconds(timeToSpawnBugs);

        Instantiate(bugMonster, bugSpawnPoint.position, bugSpawnPoint.rotation);

        anim.SetBool("Roar", false);
        yield return new WaitForSeconds(timeToSpawnBugs);

        currentState = BossStates.Waiting;
        ChangeState();
    }

    IEnumerator RollCoroutine()
    {
        anim.SetBool(ROLL, true);
        yield return new WaitForSeconds(timeToRoll);
        collisioned = false;

        while (!collisioned)
        {
            transform.Translate(rollSpeed * Time.deltaTime * Vector3.left, Space.Self);
            yield return null;
        }

        if (transform.position.x < player.position.x)
        {
            rb.AddForce(Vector3.left * outOfRollThrowForce);
        }
        else
        {
            rb.AddForce(Vector3.right * outOfRollThrowForce);
        }

        yield return new WaitForSeconds(outOfRollThrowTime);
        rb.linearVelocity = Vector2.zero;

        anim.SetBool(ROLL, false);

        yield return new WaitForSeconds(timeToRoll);
        currentState = BossStates.Waiting;
        ChangeState();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var contactPointNormal = collision.GetContact(collision.contacts.Length - 1).normal;

        if (contactPointNormal.x is > 0.5f or < -0.5f)
        {
            collisioned = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage(damage);

            if (transform.position.x < player.position.x) // Derecha
            {
                playerController.GetKnockedBack(new Vector3(1, 0.5f) * knockbackForce);
            }
            else // Izquierda
            {
                playerController.GetKnockedBack(new Vector3(-1, 0.5f) * knockbackForce);
            }

            rb.linearVelocity = Vector2.zero;
            StartCoroutine(collision.gameObject.GetComponent<PlayerController>().KnockBackCoroutine());
        }
    }

    IEnumerator SpikesCoroutine()
    {
        anim.SetBool(SPIKES, true);

        yield return new WaitForSeconds(spikesTime);

        ShootSpikes();

        yield return new WaitForSeconds(tiredTime);

        anim.SetBool(SPIKES, false);
        currentState = BossStates.Waiting;
        ChangeState();

        yield return null;
    }

    public void ShootSpikes()
    {
        for (int i = 0; i < spikesSpawnPoints.Length; i++)
        {
            Instantiate(spikesPrefab, spikesSpawnPoints[i].position, spikesSpawnPoints[i].rotation);
        }
    }

    public void TakeDamage(float damage)
    {
        bossLife -= damage;
        collisioned = true;
        if (bossLife <= 0) // Muerte
        {
            anim.SetTrigger(DEATH);
            GetComponent<Collider2D>().enabled = false;
            rb.gravityScale = 0;
            enabled = false;
            GameManager.Instance.GameDataObject.Boss1Defeated = true;
            StopAllCoroutines();
        }
        else
        {
            if (anim.GetBool(JUMPING))
            {
                anim.SetTrigger(HIT_AIR);
            }
            else
            {
                TookDamage?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}