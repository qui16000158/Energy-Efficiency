﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public QuizQuestion[] questions;

    private int questionNumber;

    private int correctQuestionAnswer;

    private int questionScore;

    private int userAnswer;

    private int totalScore;

    public TextMeshProUGUI questionText;

    public TextMeshProUGUI a;

    public TextMeshProUGUI b;

    public TextMeshProUGUI c;

    public TextMeshProUGUI trueOrFalse;

    public TextMeshProUGUI scoreText;

    public Button buttonA;

    public Button buttonB;

    public Button buttonC;

    public GameObject startButton;

    public GameObject answerButtonsParent;

    public GameObject nextButton;

    public GameObject instructions;

    public GameObject highScoreManager;

    public GameObject quizCanvas;

    public GameObject scoreNameInput;

    public GameObject addNameToListButton;

    public GameObject scoreLoadButton;

    public GameObject scoreList;

    public GameObject avatarWindows;

    public AudioSource audioSource;

    public AudioClip startQuizSound;

    public AudioClip rightAnswerSound;

    public AudioClip wrongAnswerSound;

    public AudioClip endQuizSound;


    private void Start()
    {
        questionNumber = 0;
    }

    public void StartQuiz()
    {
        instructions.SetActive(false);   

        startButton.SetActive(false);

        AnswerButtonsVisibleTrue();

        LoadNextQuestion();

        PlayStartQuizSound();
    }

    public void LoadNextQuestion() //Method used to load the next questions from the Questions Array. Questions are stored as Scriptable Objects.
    {
        if (questions.Length > 0 && questionNumber < questions.Length - 1) //Compares array lenght (number of questions) to current question number to check if there is more questions to load.
        {
            questionText.text = questions[questionNumber].questionText;

            a.text = questions[questionNumber].answer_A;

            b.text = questions[questionNumber].answer_B;

            c.text = questions[questionNumber].answer_C;

            correctQuestionAnswer = questions[questionNumber].correctAnswer;

            questionScore = questions[questionNumber].scoreAmount;

            AnswerButtonsInteractableTrue();

            ResetButtonColor();

            trueOrFalse.gameObject.SetActive(false);

            nextButton.SetActive(false);
        }
        else if (questionNumber == questions.Length -1) //If all questions have been answered, end quiz
        {
            EndQuiz();
        }
    }


    public void AnswerA() //Button A pressed
    {
        userAnswer = 1;
        Answered();
    }

    public void AnswerB() //Button B pressed
    {
        userAnswer = 2;
        Answered();
    }

    public void AnswerC() //Button C pressed
    {
        userAnswer = 3;
        Answered();
    }


    public void Answered() //This method runs after the answer has been provided. 
    {
        questionNumber += 1;

        ChangeButtonColor();

        trueOrFalse.gameObject.SetActive(true);

        AnswerButtonsInteractableFalse();

        nextButton.SetActive(true);

        if (userAnswer == correctQuestionAnswer) // If correct 
        {
            AddScore(questionScore);

            trueOrFalse.text = "Correct!" + "\n" + "+" + questionScore;

            trueOrFalse.color = Color.green;

            PlayRightAnswerSound();
        }
        else
        {
            trueOrFalse.text = "False! ";

            trueOrFalse.color = Color.red;

            PlayWrongAnswerSound();
        }
    }

    private void ChangeButtonColor() //Changes the colour of all A B C buttons colour. Button with correct Answer turns green and the rest turn red.
    {
        if(correctQuestionAnswer == 1)
        {
            buttonA.image.color = Color.green;
        }
        else
        {
            buttonA.image.color = Color.red;
        }

        if (correctQuestionAnswer == 2)
        {
            buttonB.image.color = Color.green;
        }
        else
        {
            buttonB.image.color = Color.red;
        }

        if (correctQuestionAnswer == 3)
        {
            buttonC.image.color = Color.green;
        }
        else
        {
            buttonC.image.color = Color.red;
        }
    }

    private void ResetButtonColor() //Resets A B C button color to white when new question loads
    {
        buttonA.image.color = Color.white;

        buttonB.image.color = Color.white;

        buttonC.image.color = Color.white;
    }

    public void AnswerButtonsInteractableTrue() //Enables A B C button clicks
    {
            buttonA.interactable = true;

            buttonB.interactable = true;

            buttonC.interactable = true;
    }

    public void AnswerButtonsInteractableFalse() //Disables A B C button clicks
    {
            buttonA.interactable = false;

            buttonB.interactable = false;

            buttonC.interactable = false;       
    }

    public void AnswerButtonsVisibleTrue() //Show A B C buttons
    {
        answerButtonsParent.SetActive(true);
    }

    public void AnswerButtonsVisibleFalse() //Hide A B C buttons
    {
        answerButtonsParent.SetActive(false);
    }

    private void AddScore(int scoreAmount) //Method used to add score within the HighScore manager
    {
        highScoreManager.GetComponent<HighScore>().IncreaseScore(scoreAmount);

        totalScore += scoreAmount;
    }

    private void HighScoreVisibleTrue() //Method used to enable the highscore visibility on canvas
    {
        scoreNameInput.SetActive(true);

        addNameToListButton.SetActive(true);

        scoreList.SetActive(true);

        scoreLoadButton.SetActive(true);      
    }

    private void HighScoreVisibleFalse() //Method used to disable the highscore visibility on canvas
    {
        scoreNameInput.SetActive(false);

        addNameToListButton.SetActive(false);

        scoreList.SetActive(false);

        scoreLoadButton.SetActive(false);
    }

    private void EndQuiz() //Method used to end the quiz, it will display your score, enable end game UI like highscore
    {
        questionText.text = "This is the end of the Quiz." + "\n" + "You have scored: " + totalScore + " points";

        AnswerButtonsInteractableFalse();

        AnswerButtonsVisibleFalse();

        nextButton.SetActive(false);

        trueOrFalse.gameObject.SetActive(false);

        HighScoreVisibleTrue();

        PlayEndQuizSound();
    }

    public void ResetQuiz() //This method is used to reset the quiz, it will be executed whenever the player closes the quiz or restarts it using the button, it resets the quiz including score and current question number
    {
        questionNumber = 0;

        totalScore = 0;

        AnswerButtonsVisibleFalse();

        AnswerButtonsInteractableFalse();

        questionText.text = "Welcome to the Energy Awareness Quiz";

        startButton.SetActive(true);

        instructions.SetActive(true);

        HighScoreVisibleFalse();

        highScoreManager.GetComponent<HighScore>().ResetScore();

        trueOrFalse.gameObject.SetActive(false);

        nextButton.SetActive(false);
    }

    public void OpenQuiz() //Method used to make the quiz enabled on the canvas
    {
        quizCanvas.SetActive(true);

        ResetQuiz();
    }

    public void ExitQuiz() //Method used to make the quiz disabled on the canvas
    {               
        ResetQuiz();

        quizCanvas.SetActive(false);
    }

    public void AvatarWindowsEnabled()
    {
        avatarWindows.SetActive(true);
    }

    public void AvatarWindowsDisabled()
    {
        avatarWindows.SetActive(false);
    }

    public void PlayStartQuizSound()
    {
        audioSource.PlayOneShot(startQuizSound);
    }

    public void PlayRightAnswerSound()
    {
        audioSource.PlayOneShot(rightAnswerSound);
    }

    public void PlayWrongAnswerSound()
    {
        audioSource.PlayOneShot(wrongAnswerSound);

    }

    public void PlayEndQuizSound()
    {
        audioSource.PlayOneShot(endQuizSound);
    }
}
