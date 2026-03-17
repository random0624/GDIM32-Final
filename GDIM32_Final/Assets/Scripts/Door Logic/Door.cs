using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool canOpen;

    [SerializeField] private Inventory playerInvetory;
    [SerializeField] private TMP_Text doorText;

    public delegate void ListenToEvent();
    public event ListenToEvent wrongKey;
    public event ListenToEvent correctKey;
    public event ListenToEvent finalDoor;

    private int doorsOponed = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.Player.doorClickedOn += doorLogic;
    }

    // Update is called once per frame
    void Update()
    {
       CheckIndex();
       CheckIfOpen(this);
    }
    
    
    private void doorLogic()
    {
        int index = playerInvetory.selectedItemIndex; 
        itemType currentItem = playerInvetory.inventoryList[index].itemType;

        if (currentItem == itemType.Key)
        {
            canOpen = true;
            playerInvetory.RemoveItem(currentItem);
            correctKey?.Invoke();

        } else if (index == 0)
        {
            wrongKey?.Invoke();
        }
        else
        {
            wrongKey?.Invoke();
        }
    }

    private void CheckIfOpen(Door store)
    {
        if (canOpen == true)
        {
                doorText.gameObject.SetActive(true);

                StartCoroutine(waitSeconds());

                doorText.gameObject.SetActive(false);
                canOpen = false;
                store.gameObject.transform.position = this.transform.position + Vector3.forward / 2;
                doorsOponed++;
        }
    }
    IEnumerator waitSeconds()
    {
        yield return new WaitForSeconds(3);
    }

    private void CheckIndex()
    {
        if(doorsOponed >= 2)
        {
            finalDoor?.Invoke();
            this.gameObject.SetActive(false);
        }
    }

}
