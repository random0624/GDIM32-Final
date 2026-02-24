using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableData _itemData;

    public void PickUp()
    {
        if (_itemData == null) return;
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
            inventory.AddItem(_itemData);
        Destroy(gameObject);
    }

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