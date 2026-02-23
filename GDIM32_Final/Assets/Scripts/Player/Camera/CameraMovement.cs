using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraPlace;

    private void Update()
    {
        transform.position = cameraPlace.position;
    }
}
