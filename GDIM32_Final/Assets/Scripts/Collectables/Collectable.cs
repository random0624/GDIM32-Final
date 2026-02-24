using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableData _itemData;

    //PickUp is called when the player picks up the collectable
    public void PickUp()
    {
        if (_itemData == null) return;
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
            inventory.AddItem(_itemData);
        Destroy(gameObject);
    }

    //OnTriggerEnter is called when the player's collider touches the collectable's collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
                player.SetNearbyCollectable(this);
        }
    }

    //OnTriggerExit is called when the player's collider stops touching the collectable's collider
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