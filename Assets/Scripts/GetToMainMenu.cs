using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetToMainMenu : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
        }
    }
}