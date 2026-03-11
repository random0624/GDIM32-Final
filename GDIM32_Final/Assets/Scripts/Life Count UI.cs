using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static PlayerMovement;

public class LifeCountUI : MonoBehaviour
{

    [SerializeField] private TMP_Text _lifeCountText;
    private int _lifeCount;
    // Start is called before the first frame update
    void Start()
    {
        _lifeCount =GameController.Instance.Player._lifeCount;
        _lifeCountText.text = "Lives:" + _lifeCount;
    }

    // Update is called once per frame
    void Update()
    {
        



    }

    private void LoseLife()
    {
        _lifeCount--;
        _lifeCountText.text = "Lives: " + _lifeCount;
    }
    void OnEnable()
    {
        FindObjectOfType<PlayerMovement>().OnLoseLife += LoseLife;
    }

    void OnDisable()
    {
        FindObjectOfType<PlayerMovement>().OnLoseLife -= LoseLife;
    }

    
}
