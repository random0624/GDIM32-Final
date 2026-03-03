using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("General")]
    public List<itemType> inventoryList;
    public int selectedItemIndex;

    public int _meatCount;
    [SerializeField] GameObject meatPrefab;

    [Space(20)]
    [Header("Keys")]
    [SerializeField] KeyCode throwItemKey;
    [SerializeField] KeyCode useItemKey;

    [Space(20)]
    [Header("ItemGameObjects")]
    [SerializeField] GameObject keyItem;
    [SerializeField] GameObject meatItem;

    [Space(20)]
    [Header("Raycast Pickup")]
    [SerializeField] Camera playerCamera;
    [SerializeField] float pickupReach = 5f;

    private Dictionary<itemType, GameObject> itemSetActive = new Dictionary<itemType, GameObject>(){};
    private Dictionary<itemType, GameObject> itemPrefab = new Dictionary<itemType, GameObject>(){};
    void Start()
    {
        itemSetActive.Add(itemType.Key, keyItem);
        itemSetActive.Add(itemType.Meat, meatItem);

        itemPrefab.Add(itemType.Meat, meatPrefab);
        if(inventoryList == null){
            inventoryList = new List<itemType>();
        }
        inventoryList.Clear();
        NewItemSelected();
    }
    void Update()
    {

        /*
        if(Input.GetKeyDown(throwItemKey) && inventoryList.Count > 0){
            Instantiate(itemPrefab[inventoryList[selectedItemIndex]], position: meatPrefab.transform.position, new Quaternion());
            inventoryList.RemoveAt(selectedItemIndex);
            if(selectedItemIndex != 0){
                selectedItemIndex--;
            }
            NewItemSelected();
        }
        */

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
        if (playerCamera == null) return;

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f));
        RaycastHit hitInfo;

        if (!Physics.Raycast(ray, out hitInfo, pickupReach))
            return;

        Collectable collectable = hitInfo.collider.GetComponent<Collectable>();
        if (collectable == null) return;

        AddItem(collectable.ItemData);
        Destroy(collectable.gameObject);
    }

    public void AddItem(CollectableData data)
    {
        if (data == null) return;
        if (inventoryList == null)
        {
            inventoryList = new List<itemType>();
        }

        inventoryList.Add(data.itemType);
        NewItemSelected();
    }

    private void NewItemSelected()
    {
        keyItem.SetActive(false);
        meatItem.SetActive(false);

        if (inventoryList == null || inventoryList.Count == 0)
            return;

        GameObject selectedItem = itemSetActive[inventoryList[selectedItemIndex]];
        selectedItem.SetActive(true);
    }

    public int CountItem(itemType type)
    {
        int count=0;
        if (inventoryList == null) return 0;
        else {

            
            for (int i = 0; i < inventoryList.Count; i++)
            {
                if (inventoryList[i] == type)
                {
                    count++;
                }
            }
            return count;
          }


    }

    public void RemoveItem(itemType type)
    {
        if (inventoryList == null) return;
        else
        {
            for (int i = 0; i < inventoryList.Count; i++)
            {
                if (inventoryList[i] == type)
                {
                    inventoryList.RemoveAt(i);
                    if (selectedItemIndex >= inventoryList.Count)
                        selectedItemIndex = Mathf.Max(0, inventoryList.Count - 1);

                    NewItemSelected(); 
                    
                    return;
                }
            }
            return;
        }
    }
}