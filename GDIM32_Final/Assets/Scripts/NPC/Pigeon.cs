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

    [SerializeField] private DialogueData hint;

    private bool isOpeoned = false;
    private bool nextText = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateText();
        if (Input.GetKeyDown(KeyCode.V))
        {
            nextStage();
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            closeBox();
        }
        checkBox();
    }

    private void OnMouseDown()
    {
        if (!isOpeoned)
        {
            isOpeoned = true;
        }

    }

    private void checkBox()
    {
        if (isOpeoned)
        {
            dialougeBox.SetActive(true);
        }
        if(!isOpeoned)
        {
            dialougeBox.SetActive(false);
        }
    }

    public void closeBox()
    {
       isOpeoned = false;
       optionButton.gameObject.SetActive(true);
       nextText = false;
    }

    public void nextStage()
    {
        optionButton.gameObject.SetActive(false);
        nextText = true;
    }

    public void updateText()
    {
        if(!nextText)
        {
            hintText.text = hint.startingText;
        }
        if(nextText)
        {
            hintText.text = hint.finalText;
        }
    }

}
