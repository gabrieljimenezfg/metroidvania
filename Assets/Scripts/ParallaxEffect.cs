using System;
using Unity.VisualScripting;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeField] private float movementPercentage;
    private Vector3 previousPosition;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        previousPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
       Vector3 displacementThisFrame = cameraTransform.position - previousPosition; 
       transform.Translate(displacementThisFrame * movementPercentage);
       previousPosition = cameraTransform.position;
    }
}
