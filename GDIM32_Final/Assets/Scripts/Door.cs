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
        //GameObject obj = currentItem;
        //if (obj.tag == "key")
        {
            //Check if the color of this key is the same as door
            //Key key = null; // obj.GetComponent<doorLogic>();
            //if (key.color == color)
            {
                canOpen = true;
            }

        }
    }

    private void CheckIfOpen(Door store)
    {
        if (canOpen == true)
        {
            this.gameObject.SetActive(false);
        }
    }
    
}
