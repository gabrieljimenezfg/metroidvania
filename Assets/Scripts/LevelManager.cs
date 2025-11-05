using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Image lifeBar, manaBar;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        UpdateLife();
        UpdateMana();

        var player = GameObject.FindGameObjectWithTag("Player");
        Transform spawnPoint;
        if (GameManager.Instance.isLoadingGame)
        {
            GameManager.Instance.isLoadingGame = false;
            var saveStoneObject = FindFirstObjectByType<SaveStone>();
            spawnPoint = saveStoneObject.transform;
        }
        else
        {
            spawnPoint = spawnPoints[GameManager.Instance.doorToGo];
        }

        player.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
    }

    public void UpdateLife()
    {
        lifeBar.fillAmount = GameManager.Instance.GameDataObject.PlayerCurrentLife /
                             GameManager.Instance.GameDataObject.PlayerMaxLife;
    }

    public void UpdateMana()
    {
        manaBar.fillAmount = GameManager.Instance.GameDataObject.PlayerCurrentMana /
                             GameManager.Instance.GameDataObject.PlayerMaxMana;
    }
}