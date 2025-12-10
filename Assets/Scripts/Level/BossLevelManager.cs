using System;
using UnityEngine;

public class BossLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject congratsText;
    [SerializeField] private BossController bossController;
    [SerializeField] private MusicManager musicManager;

    public void ShowCongrats()
    {
        congratsText.SetActive(true);
        Invoke(nameof(HideCongrats), 5f);
    }

    public void StartFight()
    {
        bossController.WakeUp();
        musicManager.Play();
    }

    private void HideCongrats()
    {
        congratsText.SetActive(false);
    }
}