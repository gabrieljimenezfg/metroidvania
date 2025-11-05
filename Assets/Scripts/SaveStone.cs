using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveStone : InteractableObject
{
    private void Update()
    {
        if (!isPlayerInInteractionArea) return;

        var verticalInput = Input.GetAxis("Vertical");
        if (verticalInput >= 0.5f)
        {
            GameManager.Instance.GameDataObject.SceneIndex = SceneManager.GetActiveScene().buildIndex;
            GameManager.Instance.SaveGame();
            SetIsInteractionAllowed(false);
        }
    }
}