using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionSpotlight : MonoBehaviour
{

   [SerializeField] private GameObject _lion;
    private Transform _lionTransform;
    // Start is called before the first frame update
    void Start()
    {
        _lionTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _lionTransform.position;
    }
}
