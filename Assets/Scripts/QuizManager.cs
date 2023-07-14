using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private List<Questions> questionsList;
    [SerializeField] private GameObject[] answersTextOptions;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private int currentQuestion;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private int score;
    [SerializeField] private GameObject quizPanel;
    [SerializeField] private GameObject gameOverPanel;
    private Color greenColor;
    private Color redColor;
    private Answer answer;
    private int finalScore;
    private int bestsCore;

    // Will generate a random question
    private void Start()
    {
        quizPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        GenerateQuestion();
    }

    // CurrentQuestion Generates the Random Question, and puts the question in the TMPro text
    private void GenerateQuestion()
    {
        if (questionsList.Count > 0)
        {
            currentQuestion = Random.Range(0, questionsList.Count);
            questionText.text = questionsList[currentQuestion].question;
            PutAnswer();
            SetButtonState();
        }
        else
        {
            GameOver();
        }
        scoreText.text = "Score: " + score.ToString();
        finalScore = score;
        bestScoreText.text = "Best Score: " + bestsCore.ToString();
        bestsCore = PlayerPrefs.GetInt("bestScore", 0);
    }

    // The for takes the 4 buttons text, and puts the answers options in their current positions
    private void PutAnswer()
    {
        for (int i = 0; i < answersTextOptions.Length; i++)
        {
            answersTextOptions[i].GetComponent<Image>().color = answersTextOptions[i].GetComponent<Answer>().originalColor;
            answersTextOptions[i].GetComponentInChildren<TextMeshProUGUI>().text = questionsList[currentQuestion].answers[i];
            answersTextOptions[i].GetComponent<Button>().interactable = true;
        }
    }

    public void SetButtonState()
    {
        for (int i = 0; i < answersTextOptions.Length; i++)
        {
            answersTextOptions[i].GetComponent<Answer>().isCorrect = false;

            if (answersTextOptions[i].gameObject.name == questionsList[currentQuestion].correctAnswer.ToString())
            {
                answersTextOptions[i].GetComponent<Answer>().isCorrect = true;
            }
        }
    }

    public void CheckCorrectAnswer()
    {
        for (int i = 0; i < answersTextOptions.Length; i++)
        {
            if (answersTextOptions[i].GetComponent<Answer>().isCorrect)
            {
                answersTextOptions[i].GetComponent<Image>().color = Color.green;
            }
        }
    }

    private IEnumerator ChangeColorTillNextQuestion()
    {
        DisableButtons();
        yield return new WaitForSeconds(0.5f);
        GenerateQuestion();
    }

    private void DisableButtons()
    {
        for (int i = 0; i < answersTextOptions.Length; i++)
        {
            answersTextOptions[i].GetComponent<Button>().interactable = false;
        }
    }

    private void GameOver()
    {
        quizPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        if (score >= 0)
        {
            finalScoreText.text = "Final Score: " + score.ToString();
        }
        else
        {
            finalScoreText.text = "Final Score: 0";
        }
    }

    // When answered the question removes the previous question and generates another
    public void CorrectAnswer()
    {
        score += 100;
        if (bestsCore < score)
        {
            PlayerPrefs.SetInt("bestScore", score);
        }
        Debug.Log("Score now is: " + score);
        StartCoroutine(ChangeColorTillNextQuestion());
        questionsList.RemoveAt(currentQuestion);
    }

    public void WrongAnswer()
    {
        score -= 25;
        Debug.Log("Score now is: " + score);
        StartCoroutine(ChangeColorTillNextQuestion());
        questionsList.RemoveAt(currentQuestion);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit's the game");
    }

    public void EndGame()
    {
        quizPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        // finalScoreText.text = "Final Score: " + score.ToString();

        if (score >= 0)
        {
            finalScoreText.text = "Final Score: " + score.ToString();
        }
        else
        {
            finalScoreText.text = "Final Score: 0";
        }
    }
}

