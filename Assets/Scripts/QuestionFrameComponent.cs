using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionFrameComponent : MonoBehaviour
{
    public TMP_Text[] answerTexts;
    public TMP_Text questionText;
    [HideInInspector]
    public bool shouldReloadQuestion = false;
    public Color defaultButtonColor;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //each time new question is asked the texts should be reloaded to display the proper question
        if (shouldReloadQuestion)
        {
            SetupTexts(FindObjectOfType<QuestionsManagerComponent>());
        }
    }

    public void SetupTexts(QuestionsManagerComponent questionsManager)
    {
        //clears everything from the last question
        ClearButtons();
        
        //chooses which of the questions will be correct
        int randomIndex = Random.Range(0, 4);

        //setups the corect answer
        questionText.text = questionsManager.chosenQuestion.questionText;
        answerTexts[randomIndex].text = questionsManager.chosenQuestion.correctAnswer;
        answerTexts[randomIndex].GetComponentInParent<AnswerButtonComponent>().isCorrectAnswer = true;
        int indexOfAnswer = 0;

        //setups the other answers
        for (int i = 0; i < 4; i++)
        {
            if (i == randomIndex)
            {
                continue;
            }
            answerTexts[i].GetComponentInParent<AnswerButtonComponent>().isCorrectAnswer = false;
            answerTexts[i].text = questionsManager.chosenQuestion.wrongAnswers[indexOfAnswer];
            indexOfAnswer++;
        }

        FindObjectOfType<AIPlayerComponent>().ChooseAnswer();
    }

    void ClearButtons()
    {
        //clears buttons to their default state
        for (int i = 0; i < 4; i++)
        {
            answerTexts[i].GetComponentInParent<Image>().color = defaultButtonColor;
            answerTexts[i].GetComponentInParent<Image>().material = null;
        }
    }
}
