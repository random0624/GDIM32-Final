using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool canOpen;

    [SerializeField] private Inventory playerInvetory;
    [SerializeField] private TMP_Text doorText;

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

        int index = playerInvetory.selectedItemIndex;
        itemType currentItem = playerInvetory.inventoryList[index];

        if (currentItem == itemType.Key)
        {
            canOpen = true;
        } else if (index == 0)
        {
            Debug.Log("You dont have an item");
        }
        else
        {
            Debug.Log("Not The right item");
        }
    }

    private void CheckIfOpen(Door store)
    {
        if (canOpen == true)
        {
            store.gameObject.SetActive(false);
            doorText.gameObject.SetActive(true);
        }
        else
        {
            doorText.gameObject.SetActive(false);
        }
    }
    
}
