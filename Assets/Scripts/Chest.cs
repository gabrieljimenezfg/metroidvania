using System;
using UnityEngine;

public enum GemType
{
    DoubleJump,
    TripleJump,
    Dash,
    ExtraDamage,
}

public class Chest : InteractableObject
{
    private Animator animator;
    [SerializeField] private GemType gemType;
    [SerializeField] private ParticleSystem particleSystem;

    private void CheckAlreadyPickedUpUpgrades()
    {
        switch (gemType)
        {
            case GemType.DoubleJump:
                if (GameManager.Instance.GameDataObject.PlayerMaxJumps > 1)
                {
                    GetComponent<Collider2D>().enabled = false;
                }
                break;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        CheckAlreadyPickedUpUpgrades();
    }

    private void Update()
    {
        if (!isPlayerInInteractionArea) return;

        if (Input.GetButtonDown("Action"))
        {
            Time.timeScale = 0;
            animator.SetTrigger("Open");
            SetIsInteractionAllowed(false);
        }
    }

    public void GetGem()
    {
        switch (gemType)
        {
            case GemType.DoubleJump:
                GameManager.Instance.GameDataObject.PlayerMaxJumps = 2;
                break;
            case GemType.TripleJump:
                break;
            case GemType.Dash:
                break;
            case GemType.ExtraDamage:
                break;
            default:
                break;
        }

        Time.timeScale = 1;
        transform.GetChild(0).gameObject.SetActive(false);
        gameObject.GetComponent<Collider2D>().enabled = false;
        // popup to let player know they got an upgrade
    }

    public void StartParticles()
    {
        particleSystem.Play();
    }
}