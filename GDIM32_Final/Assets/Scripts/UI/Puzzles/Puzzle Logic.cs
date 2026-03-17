using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleLogic : MonoBehaviour
{
    [SerializeField] private TMP_InputField _playerAnswer;
    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private GameObject key;
    [SerializeField] private string answer;
    [SerializeField] private string question;

    [SerializeField] private GameObject dialougeBox;

    public delegate void GameListen();
    public event GameListen correctAnswer;
    public event GameListen incorrectAnswer;

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
        QuestionLogic(_playerAnswer);
    }

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

}
