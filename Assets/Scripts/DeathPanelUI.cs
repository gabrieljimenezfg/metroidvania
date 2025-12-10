using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathPanelUI : MonoBehaviour
{
    [SerializeField] private Button loadButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        loadButton.onClick.AddListener(() =>
        {
            GameManager.Instance.LoadGame();
            GameManager.Instance.isLoadingGame = true;
            SceneManager.LoadScene(GameManager.Instance.GameDataObject.SceneIndex);
        });
        mainMenuButton.onClick.AddListener(() => { SceneManager.LoadScene(0); });
    }
}