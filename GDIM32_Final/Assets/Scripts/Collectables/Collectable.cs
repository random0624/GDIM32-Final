using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableData _itemData;

    public CollectableData ItemData => _itemData;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
                player.SetNearbyCollectable(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
                player.SetNearbyCollectable(null);
        }
    }
}