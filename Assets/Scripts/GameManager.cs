using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameData gameData;
    public int saveSlot;
    public int doorToGo;
    public bool isLoadingGame;
    public bool isGamePaused = false;

    public event EventHandler GamePaused;
    public event EventHandler GameUnpaused;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePauseGame();
        }
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            GamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            GameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public GameData GameDataObject
    {
        get => gameData;
        set => gameData = value;
    }

    public void SaveGame()
    {
        var saveName = "data" + saveSlot;
        Debug.Log("guardando en " + saveName);
        var data = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString(saveName, data);
    }

    public void LoadGame()
    {
        var saveName = "data" + saveSlot;
        Debug.Log("LoadGame");
        Debug.Log(saveName);
        if (PlayerPrefs.HasKey(saveName))
        {
            var data = PlayerPrefs.GetString(saveName);
            gameData = JsonUtility.FromJson<GameData>(data);
        }
    }
}