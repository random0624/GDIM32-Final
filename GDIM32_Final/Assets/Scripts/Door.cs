using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string color;

    [SerializeField] private bool canOpen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfOpen(this);
    }

    
    private void OnMouseDown()
    {
        Inventory playerInventory = GetComponent<Inventory>();
        int currentIntSlot = playerInventory.selectedItemIndex;
        if (playerInventory.inventoryList[currentIntSlot] == itemType.Key) 
        {
            canOpen = true;
        }
    }

    private void CheckIfOpen(Door store)
    {
        if (canOpen == true)
        {
            store.gameObject.SetActive(false);
        }
    }
    
}
