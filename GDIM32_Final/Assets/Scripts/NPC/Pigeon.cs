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

    private int currentIndex = 0;

    private bool isOpeoned = false;
    private bool nextText = false;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.Player.birdClickedOn += OpenBox;
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

    private void OpenBox()
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
            hintText.text = hint.startingText[currentIndex];
        }
        if(nextText)
        {
            hintText.text = hint.finalText[currentIndex];
        }
    }

    public void currentText()
    {
        currentIndex++;
    }

}
