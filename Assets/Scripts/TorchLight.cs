using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TorchLight : MonoBehaviour
{
    [SerializeField] private float minScale, maxScale;
    [SerializeField] private float lightUpdateCooldown;
    private float currentLightTimer = 0;

    private void FixedUpdate()
    {
        if (currentLightTimer >= lightUpdateCooldown)
        {
            var randomScale = Random.Range(minScale, maxScale);    
            transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            currentLightTimer = 0;
        }
        
        currentLightTimer += Time.fixedDeltaTime;
    }
}
