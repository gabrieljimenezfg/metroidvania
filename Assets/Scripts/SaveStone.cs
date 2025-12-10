using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveStone : InteractableObject
{
    [SerializeField] private GameObject textObject;

    private void Update()
    {
        if (!isPlayerInInteractionArea) return;

        var verticalInput = Input.GetAxis("Vertical");
        if (verticalInput > 0f)
        {
            GameManager.Instance.GameDataObject.SceneIndex = SceneManager.GetActiveScene().buildIndex;
            GameManager.Instance.SaveGame();
            SetIsInteractionAllowed(false);
            textObject.SetActive(true);
            Invoke(nameof(Hide), 3f);
        }
    }

    private void Hide()
    {
        textObject.SetActive(false);
    }
}