using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class used for question creation
[Serializable]
public class QuestionsTemplate
{
    public string questionText;
    public string[] wrongAnswers = new string[3];
    public string correctAnswer;
}
