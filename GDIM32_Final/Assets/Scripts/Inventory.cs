using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("General")]
    public List<CollectableData> inventoryList;
    public int selectedItemIndex;

    public int _meatCount;
    [SerializeField] GameObject meatPrefab;

    [Space(20)]
    [Header("Keys")]
    //[SerializeField] KeyCode throwItemKey;
    [SerializeField] KeyCode useItemKey;

    [Space(20)]
    [Header("ItemGameObjects")]
    [SerializeField] GameObject keyItem;
    [SerializeField] GameObject meatItem;

    [Space(20)]
    [Header("Raycast Pickup")]
    [SerializeField] Camera playerCamera;
    [SerializeField] float pickupReach = 5f;

    [Space(20)]
    [Header("UI")]
    [SerializeField] Image[] inventorySlotImage = new Image[5];
    [SerializeField] Image[] inventoryBackgroundImage = new Image[5];
    [SerializeField] Sprite emptySlotSprite;
    [SerializeField] Sprite keySprite;
    [SerializeField] Sprite meatSprite;
    [SerializeField] GameObject pickUpItem_gameObject;
    [SerializeField] GameObject interactText;

    private Dictionary<itemType, Sprite> _itemSpriteByType;
    private Dictionary<itemType, GameObject> itemSetActive = new Dictionary<itemType, GameObject>(){};
    private Dictionary<itemType, GameObject> itemPrefab = new Dictionary<itemType, GameObject>(){};

    // Current selected item's data for keyId to see if it's the correct key.
    public CollectableData CurrentItem
    {
        get
        {
            if (inventoryList == null || inventoryList.Count == 0)
                return null;
            if (selectedItemIndex < 0 || selectedItemIndex >= inventoryList.Count)
                return null;
            return inventoryList[selectedItemIndex];
        }
    }

    void Start()
    {
        itemSetActive.Add(itemType.Key, keyItem);
        itemSetActive.Add(itemType.Meat, meatItem);

        itemPrefab.Add(itemType.Meat, meatPrefab);
        if(inventoryList == null){
            inventoryList = new List<CollectableData>();
        }
        for(int i = 0; i < inventorySlotImage.Length; i++){
            inventorySlotImage[i].sprite = emptySlotSprite;
        }
        inventoryList.Clear();
        _itemSpriteByType = new Dictionary<itemType, Sprite>();
        if (keySprite != null) _itemSpriteByType[itemType.Key] = keySprite;
        if (meatSprite != null) _itemSpriteByType[itemType.Meat] = meatSprite;
        NewItemSelected();
        pickUpItem_gameObject.SetActive(false);
    }
    void Update()
    {
        UpdatedPickupPrompt();
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
        // UI
        for (int i = 0; i < inventorySlotImage.Length; i++){
            if (inventorySlotImage == null || i >= inventorySlotImage.Length) continue;
            if (i < inventoryList.Count && _itemSpriteByType != null && _itemSpriteByType.TryGetValue(inventoryList[i].itemType, out Sprite sprite) && sprite != null)
                inventorySlotImage[i].sprite = sprite;
            else
                inventorySlotImage[i].sprite = emptySlotSprite;
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

        if (Physics.Raycast(ray, out hitInfo, pickupReach)){
            Collectable collectable = hitInfo.collider.GetComponent<Collectable>();
            if(collectable!=null){
                pickUpItem_gameObject.SetActive(true);
                if(Input.GetKeyDown(useItemKey)){
                    AddItem(collectable.ItemData);
                    Destroy(collectable.gameObject);
                }
            }
            else{
                    pickUpItem_gameObject.SetActive(false);
                }
        }
        else{
                    pickUpItem_gameObject.SetActive(false);
                }
    }

    private void UpdatedPickupPrompt(){
        if(pickUpItem_gameObject == null || playerCamera == null){
            if(pickUpItem_gameObject != null) pickUpItem_gameObject.SetActive(false);
            return;
        }

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f));
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, pickupReach)){
            Collectable collectable = hitInfo.collider.GetComponent<Collectable>();
            if(collectable != null){
                pickUpItem_gameObject.SetActive(true);
            }
            else{
                pickUpItem_gameObject.SetActive(false);
            }
        }
        else{
            pickUpItem_gameObject.SetActive(false);
        }
    }

    public void AddItem(CollectableData data)
    {
        if (data == null) return;
        if (inventoryList == null)
        {
            inventoryList = new List<CollectableData>();
        }

        inventoryList.Add(data);
        NewItemSelected();
    }

    private void NewItemSelected()
    {
        keyItem.SetActive(false);
        meatItem.SetActive(false);

        if (inventoryList == null || inventoryList.Count == 0)
            return;

        GameObject selectedItem = itemSetActive[inventoryList[selectedItemIndex].itemType];
        selectedItem.SetActive(true);
    }

    public int CountItem(itemType type)
    {
        int count=0;
        if (inventoryList == null) return 0;
        else {

            
            for (int i = 0; i < inventoryList.Count; i++)
            {
                if (inventoryList[i].itemType == type)
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
                if (inventoryList[i].itemType == type)
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