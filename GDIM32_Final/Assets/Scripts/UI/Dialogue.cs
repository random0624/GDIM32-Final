using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private Button _nextButton;

    [SerializeField] private TMP_Text _boxText;
 
    [SerializeField] private ScriptableObject hint;

    // Start is called before the first frame update
    void Start()
    {
        _boxText.text = hint.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnButtonPress()
    {
        _nextButton.gameObject.SetActive(false);
        _boxText.text = hint.ToString();
    }

}
