using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used to create questions containing numbers
[Serializable]
public class NumberQuestionsTemplate
{
    public string questionHeading;
    public float correctAnswer;
    public float minAIAnswer;
    public float maxAIAnswer;
    public float minAIAnswerTime;
    public float maxAIAnswerTime;
}
