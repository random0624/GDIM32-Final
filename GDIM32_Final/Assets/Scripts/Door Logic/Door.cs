using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool canOpen;

    [SerializeField] private Inventory playerInvetory;
    [SerializeField] private TMP_Text doorText;
    [SerializeField] private GameObject winText;

    [SerializeField] private Vector3[] nextLocation;

    public delegate void ListenToEvent();
    public event ListenToEvent wrongKey;
    public event ListenToEvent correctKey;
    public event ListenToEvent finalDoor;

    private bool gameEnded = false;

    private int doorsOponed = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.Player.doorClickedOn += doorLogic;
        GameController.Instance._gameOver += CheckGameOver;
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = nextLocation[doorsOponed];
       CheckIndex();
       CheckIfOpen(this);
       CheckGameOver();
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
                DoorTextShowup(); 
                canOpen = false;
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
            
        }
        if(doorsOponed >= 3)
        {
            this.gameObject.SetActive(false);
            winText.SetActive(true);
            gameEnded = true;
        }
    }

    private void DoorTextShowup()
    {
        doorText.gameObject.SetActive(true);

        StartCoroutine(waitSeconds());

        doorText.gameObject.SetActive(false);
    }

    private void GameOver()
    {
        gameEnded = true;
    }

    private void CheckGameOver()
    {
        if (gameEnded)
        {
            Time.timeScale = 0.0f;

        }
    }
}
