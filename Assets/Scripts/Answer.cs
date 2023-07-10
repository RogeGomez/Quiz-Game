using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    public bool isCorrect;
    public QuizManager quizManager;
    public Color originalColor;

    private void Start()
    {
        originalColor = GetComponent<Image>().color;
    }

    // Checks if the answer is correct or not
    public void Answer_()
    {
        if (isCorrect)
        {
            Debug.Log("Correct Answer");
            GetComponent<Image>().color = Color.green;
            Debug.Log("Color Green");
            quizManager.CorrectAnswer();
        }
        else
        {
            Debug.Log("Incorrect Answer");
            GetComponent<Image>().color = Color.red;
            Debug.Log("Color Red");
            quizManager.WrongAnswer();
            quizManager.CheckCorrectAnswer();
        }
    }
}
