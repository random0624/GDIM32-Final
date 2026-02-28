using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("General")]
    public List<itemType> inventoryList;
    public int selectedItemIndex;

    [Space(20)]
    [Header("Keys")]
    [SerializeField] KeyCode throwItemKey;
    [SerializeField] KeyCode useItemKey;

    [Space(20)]
    [Header("ItemGameObjects")]
    [SerializeField] GameObject keyItem;
    [SerializeField] GameObject meatItem;

    private Dictionary<itemType, GameObject> itemSetActive = new Dictionary<itemType, GameObject>(){};
    void Start()
    {
        itemSetActive.Add(itemType.Key, keyItem);
        itemSetActive.Add(itemType.Meat, meatItem);

        NewItemSelected();
    }
    void Update()
    {
        if(Input.GetKeyDown(useItemKey))
        {
            PickUpItem();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1) && inventoryList.Count > 0)
        {
            selectedItemIndex = 0;
            NewItemSelected();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && inventoryList.Count > 1)
        {
            selectedItemIndex = 1;
            NewItemSelected();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3) && inventoryList.Count > 2)
        {
            selectedItemIndex = 2;
            NewItemSelected();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4) && inventoryList.Count > 3)
        {
            selectedItemIndex = 3;
            NewItemSelected();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5) && inventoryList.Count > 4)
        {
            selectedItemIndex = 4;
            NewItemSelected();
        }

    }

    private void PickUpItem()
    {
        if (GameController.Instance == null) return;
        if (GameController.Instance.Player == null) return;

        GameController.Instance.Player.TryPickUpNearby();
    }

    public void AddItem(CollectableData data)
    {
        if (data == null) return;
        if (inventoryList == null)
        {
            inventoryList = new List<itemType>();
        }

        inventoryList.Add(data.itemType);
    }

    private void NewItemSelected()
    {
        /*keyItem.SetActive(false);
        meatItem.SetActive(false);*/

        GameObject selectedItem = itemSetActive[inventoryList[selectedItemIndex]];
        selectedItem.SetActive(true);
    }
}