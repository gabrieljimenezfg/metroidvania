using System;
using System.Collections;
using UnityEngine;

public class BossVisual : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BossController bossController;
    private Material originalMaterial;
    [SerializeField] private Material bossHitMaterial;
    [SerializeField] private float hitFlashDuration;

    private void Awake()
    {
        bossController = GetComponent<BossController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    private void Start()
    {
        bossController.TookDamage += BossControllerOnTookDamage;
    }

    private void BossControllerOnTookDamage(object sender, EventArgs e)
    {
        StartCoroutine(FlashHit());
    }

    private IEnumerator FlashHit()
    {
        spriteRenderer.material = bossHitMaterial;
        yield return new WaitForSeconds(hitFlashDuration);
        spriteRenderer.material = originalMaterial;
    }
}