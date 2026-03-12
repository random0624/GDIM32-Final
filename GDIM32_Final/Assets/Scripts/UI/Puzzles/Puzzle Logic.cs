using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuzzleLogic : MonoBehaviour
{
    [SerializeField] private TMP_InputField _playerAnswer;
    [SerializeField] private GameObject key;
    [SerializeField] private string answer;

    public delegate void GameListen();
    public event GameListen correctAnswer;
    public event GameListen incorrectAnswer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        QuestionLogic(_playerAnswer);
    }

    private void QuestionLogic(TMP_InputField _answer)
    {
        string answerString = _answer.text.ToLower();

        if(answerString == answer)
        {
            correctAnswer?.Invoke();
        }
        else
        {
            incorrectAnswer?.Invoke();
        }
    }
}
