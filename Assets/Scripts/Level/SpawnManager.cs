using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        Transform spawnPoint;
        if (GameManager.Instance.isLoadingGame)
        {
            GameManager.Instance.isLoadingGame = false;
            var saveStoneObject = FindFirstObjectByType<SaveStone>();
            spawnPoint = saveStoneObject.transform;
        }
        else
        {
            spawnPoint = spawnPoints[GameManager.Instance.doorToGo];
        }

        player.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
    }
}