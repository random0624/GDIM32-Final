using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCycle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> doors = new List<GameObject>();

    private int index;

    void Start()
    {
        index = 0;
        GameController.Instance.CurrentDoor.correctKey += NextDoor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NextDoor()
    {
        doors[index].SetActive(false);

        index++;

        doors[index].SetActive(true);
    }
}
