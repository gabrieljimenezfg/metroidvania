using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelTrigger : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    [SerializeField] private int doorPoint;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        GameManager.Instance.doorToGo = doorPoint;
        SceneManager.LoadScene(levelIndex);
    }
}
