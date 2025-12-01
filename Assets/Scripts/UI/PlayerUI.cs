using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image lifeBar, manaBar;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        playerController.TookDamage += PlayerControllerOnTookDamage;
        playerController.UsedMana += PlayerControllerOnUsedMana;
        
        UpdateLife();
        UpdateMana();
    }

    private void UpdateLife()
    {
        lifeBar.fillAmount = GameManager.Instance.GameDataObject.PlayerCurrentLife /
                             GameManager.Instance.GameDataObject.PlayerMaxLife;
    }

    private void UpdateMana()
    {
        manaBar.fillAmount = GameManager.Instance.GameDataObject.PlayerCurrentMana /
                             GameManager.Instance.GameDataObject.PlayerMaxMana;
    }

    private void PlayerControllerOnTookDamage(object sender, EventArgs e)
    {
        UpdateLife();
    }

    private void PlayerControllerOnUsedMana(object sender, EventArgs e)
    {
        UpdateMana();
    }
}