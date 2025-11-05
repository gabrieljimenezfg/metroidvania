using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameObject keySprite;
    protected bool isPlayerInInteractionArea;

    protected void SetIsInteractionAllowed(bool isAllowed)
    {
        keySprite.SetActive(isAllowed); 
        isPlayerInInteractionArea = isAllowed;
    } 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetIsInteractionAllowed(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetIsInteractionAllowed(false);
        }
    }
}
