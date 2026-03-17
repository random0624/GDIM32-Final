using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleLogic : MonoBehaviour
{
    //[SerializeField] private TMP_InputField _playerAnswer;
    [SerializeField] private Button but1;
    [SerializeField] private Button but2;

    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private GameObject key;
    [SerializeField] private string answer;
    [SerializeField] private string question;

    [SerializeField] private GameObject dialougeBox;

    public delegate void GameListen();
    public event GameListen correctAnswer;
    public event GameListen incorrectAnswer;

    [SerializeField] private int correctIndex;

    private bool isOpeoned;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.Player.puzzleClickedOn += OpenBox;
    }

    // Update is called once per frame
    void Update()
    {
        checkBox();
        
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(correctIndex == 1)
            {
                CorrectAnswer();
                closeBox();
            }
            else
            {
                IncorrectAnswer();
                closeBox();
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (correctIndex == 1)
            {
                IncorrectAnswer();
                closeBox();
            }
            else
            {
                CorrectAnswer();
                closeBox();
            }
        }
        //QuestionLogic(_playerAnswer);
    }

    /*
    private void QuestionLogic(TMP_InputField _answer)
    {
        string answerString = _answer.text.ToLower();

        if(answerString == answer)
        {
            correctAnswer?.Invoke();
            Instantiate(key, transform.position + Vector3.forward/2, Quaternion .identity);
        }
        else
        {
            incorrectAnswer?.Invoke();
        }
    }
    */


    private void OpenBox()
    {
        if (!isOpeoned)
        {
            isOpeoned = true;
            _questionText.text = question;

        }

    }
    public void closeBox()
    {
        isOpeoned = false;
    }

    private void checkBox()
    {
        if (isOpeoned)
        {
            dialougeBox.SetActive(true);
        }
        if (!isOpeoned)
        {
            dialougeBox.SetActive(false);
        }
    }

    private void CorrectAnswer()
    {
        correctAnswer?.Invoke();
        Instantiate(key, transform.position + Vector3.forward / 2, Quaternion.identity);
    }

    private void IncorrectAnswer()
    {
        incorrectAnswer?.Invoke();
    }
}
