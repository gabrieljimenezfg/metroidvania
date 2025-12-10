using System;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GameObject explanationCanvas;
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
            case GemType.Dash:
                if (GameManager.Instance.GameDataObject.HasDash)
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
            SoundManager.Instance.PlayRuneSound();
            animator.SetTrigger("Open");
            SetIsInteractionAllowed(false);
            Time.timeScale = 0;
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
                GameManager.Instance.GameDataObject.HasDash = true;
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
        explanationCanvas.SetActive(true);
        Invoke(nameof(HideExplanation), 5f);
    }

    private void HideExplanation()
    {
        explanationCanvas.SetActive(false);
    }

    public void StartParticles()
    {
        particleSystem.Play();
    }
}