using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static PlayerMovement;

public class LifeCountUI : MonoBehaviour
{

    [SerializeField] private TMP_Text _lifeCountText;
    
    // Start is called before the first frame update
    void Start()
    {
        
        _lifeCountText.text = "Lives:" + GameController.Instance.Player._lifeCount;
    }

    // Update is called once per frame
    void Update()
    {
        



    }

    private void LoseLife()
    {
        int lives = GameController.Instance.Player._lifeCount;
        _lifeCountText.text = "Lives: " + lives;
    }
    void OnEnable()
    {
        GameController.Instance.Player.OnLoseLife += LoseLife;
    }

    void OnDisable()
    {
        GameController.Instance.Player.OnLoseLife -= LoseLife;
    }

    
}
