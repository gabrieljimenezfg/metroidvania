using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    private GameData gameData;
    public int saveSlot;
    public int doorToGo;
    public bool isLoadingGame;

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
        // DEBUG
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayerPrefs.DeleteAll();
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
