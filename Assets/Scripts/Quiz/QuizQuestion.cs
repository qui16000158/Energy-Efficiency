using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "New Question")]
public class QuizQuestion : ScriptableObject
{
    public string questionText;

    public string answer_A;

    public string answer_B;

    public string answer_C;

    public int correctAnswer; //1, 2 ,3 (refers to A, B or C)

    public int scoreAmount; //How much score for answering correctly 
}
