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

    [SerializeField] private DialogueData[] _npcLines;

    [SerializeField] private GameObject finalKey;

    private DialogueData currentNode;

    private int currentIndex;
    private int currentNodeNum = 0;

    private bool isOpeoned = false;
    private bool nextText = false;

    private bool finalDone = false;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.Player.birdClickedOn += OpenBox;
        GameController.Instance.CurrentDoor.correctKey += UpdateNodeLine;
        GameController.Instance.CurrentDoor.finalDoor += ActivateFinal;
   
     }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.V))
        {
            AdvanceDialogue();
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            closeBox();
        }
        
        checkBox();

        //Debug.Log(currentNodeNum);
    }

    private void ActivateFinal()
    {
        finalDone = true;
    }

    private void OpenBox()
    {
        if (!isOpeoned)
        {
            isOpeoned = true;
        }
        if (finalDone == true)
        {
            SpawnFinalKey();
        }

    }

    private void checkBox()
    {
        if (isOpeoned)
        {
            dialougeBox.SetActive(true);
            UpdateText();
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

    public void UpdateText()
    {
        currentNode = _npcLines[currentNodeNum];
        hintText.text = currentNode.lines[currentIndex];
    }

    public void spawnFinalKey()
    {
        Instantiate(finalKey, this.gameObject.transform.position + Vector3.forward, Quaternion.identity);
    }

    private void AdvanceDialogue()
    {
        if (currentIndex < currentNode.lines.Length - 1)
        {
            currentIndex++;
            UpdateText();
        }
        else
        {
            currentIndex = 0;
            closeBox();
        }
    }

    private void UpdateNodeLine()
    {
        currentNodeNum++;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void SpawnFinalKey()
    {
        Instantiate(finalKey, transform.position + Vector3.forward, Quaternion.identity);
    }
}
