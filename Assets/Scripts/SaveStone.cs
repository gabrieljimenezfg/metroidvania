using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveStone : MonoBehaviour
{
    [SerializeField] private GameObject arrowUp;
    private bool isPlayerInSaveArea;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            arrowUp.SetActive(true);
            isPlayerInSaveArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            arrowUp.SetActive(false);
            isPlayerInSaveArea = false;
        }
    }

    private void Update()
    {
        if (!isPlayerInSaveArea) return;

        var verticalInput = Input.GetAxis("Vertical");
        if (verticalInput >= 0.5f)
        {
            GameManager.Instance.GameDataObject.SceneIndex = SceneManager.GetActiveScene().buildIndex;
            GameManager.Instance.SaveGame();
            isPlayerInSaveArea = false;
        }
    }
}