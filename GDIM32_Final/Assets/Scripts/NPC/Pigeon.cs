using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pigeon : MonoBehaviour
{
    [SerializeField] private GameObject dialougeBox;

    [SerializeField] private TMP_Text hintText;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button ExitButton;

    [SerializeField] private ScriptableObject hint;

    private bool isOpeoned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Box();
    }

    private void OnMouseDown()
    {
        if (!isOpeoned)
        {
            isOpeoned = true;
        }

    }

    private void Box()
    {
        if (isOpeoned)
        {
            dialougeBox.SetActive(true);
            //hintText.text = hint.ToString();
            hintText.text = "This is a test";
        }
        else
        {
            dialougeBox.SetActive(false);
        }
    }

    public void closeBox()
    {
        isOpeoned = false;
       
    }
}
