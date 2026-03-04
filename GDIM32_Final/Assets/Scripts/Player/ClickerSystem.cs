using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerSystem : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public delegate void GameListen();
    public event GameListen doorClickedOn;


    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject.name == "Door")
                {
                    doorClickedOn?.Invoke();
                }
            }
        }
    }
}
