using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => { GameManager.Instance.TogglePauseGame(); });
        mainMenuButton.onClick.AddListener(() => { SceneManager.LoadScene(0); });
    }

    private void Start()
    {
        GameManager.Instance.GamePaused += InstanceOnGamePaused;
        GameManager.Instance.GameUnpaused += InstanceOnGameUnpaused;

        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void InstanceOnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void InstanceOnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }
}