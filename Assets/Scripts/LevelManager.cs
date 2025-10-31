using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Image lifeBar, manaBar;

    private void Start()
    {
        UpdateLife();
        UpdateMana();
    }

    public void UpdateLife()
    {  
        lifeBar.fillAmount = GameManager.Instance.GameDataObject.PlayerCurrentLife / GameManager.Instance.GameDataObject.PlayerMaxLife;
    }
    
    public void UpdateMana()
    {  
        manaBar.fillAmount = GameManager.Instance.GameDataObject.PlayerCurrentMana / GameManager.Instance.GameDataObject.PlayerMaxMana;
    }
}
