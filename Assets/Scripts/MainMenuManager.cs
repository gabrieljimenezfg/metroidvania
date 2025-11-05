using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject slotsPanel;

    public void HandleStartClick()
    {
        slotsPanel.SetActive(true);
    }

    public void HandleSlotClick(int slot)
    {
        var saveName = "data" + slot;
        Debug.Log("Buscando save " + saveName);
        if (PlayerPrefs.HasKey(saveName))
        {
            Debug.Log("LoadGame" + slot);
            GameManager.Instance.saveSlot = slot;
            GameManager.Instance.LoadGame();
            GameManager.Instance.isLoadingGame = true;
            SceneManager.LoadScene(GameManager.Instance.GameDataObject.SceneIndex);
        }
        else
        {
            Debug.Log("No se ha encontrado el save " + saveName);
            GameManager.Instance.GameDataObject = new GameData();
            GameManager.Instance.saveSlot = slot;
            GameManager.Instance.GameDataObject.PlayerCurrentLife = 100;
            GameManager.Instance.GameDataObject.PlayerMaxLife = 100;
            GameManager.Instance.GameDataObject.PlayerCurrentMana = 50;
            GameManager.Instance.GameDataObject.PlayerMaxMana = 50;
            GameManager.Instance.GameDataObject.FireballDamage = 15;
            GameManager.Instance.GameDataObject.PlayerDamage = 25;
            GameManager.Instance.GameDataObject.HeavyDamage  = 50;
            GameManager.Instance.GameDataObject.PlayerMaxJumps  = 1;
            
            SceneManager.LoadScene(1);
        }
    }
}
