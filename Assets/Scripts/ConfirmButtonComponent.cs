using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConfirmButtonComponent : MonoBehaviour
{
    private NumberQuestionManagerComponent numberQuestionManagerComponent;
    public TMP_InputField inputField;
    public GameObject numberQuestionResultFrame;
    public QuestionCountDownComponent questionCountDownComponent;
    // Start is called before the first frame update
    void Start()
    {
        numberQuestionManagerComponent = FindObjectOfType<NumberQuestionManagerComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            ConfirmAnswer();
        }

        //if question time runs out and player doesn't asnwer then the question is asnwered for him
        if (questionCountDownComponent.currentQuestionTime <= 0)
        {
            if (inputField.text.Length > 0)
            {
                return;
            }
            else
            {
                inputField.text = "0";
                ConfirmAnswer();
            }

        }
      
    }

    public void ConfirmAnswer()
    {
        //confirms the player answers and sets all the values neccessary to decide who the winner of this question is
        numberQuestionManagerComponent.playerAnswer = float.Parse(inputField.text);
        numberQuestionResultFrame.SetActive(true);
        NumberQuestionResultFrameComponent numberQuestionFrameComponent = numberQuestionResultFrame.GetComponent<NumberQuestionResultFrameComponent>();
        numberQuestionFrameComponent.playerTime = FindObjectOfType<NumberQuestionFrameComponent>().playerAnswerTime;
        numberQuestionFrameComponent.playerAnswer = numberQuestionManagerComponent.playerAnswer;
        numberQuestionFrameComponent.aiAnswer = Random.Range(numberQuestionManagerComponent.chosenQuestion.minAIAnswer, numberQuestionManagerComponent.chosenQuestion.maxAIAnswer);
        numberQuestionFrameComponent.aiTime = Random.Range(numberQuestionManagerComponent.chosenQuestion.minAIAnswerTime, numberQuestionManagerComponent.chosenQuestion.maxAIAnswerTime);
        questionCountDownComponent.currentQuestionTime = questionCountDownComponent.maxQuestionTime;
        numberQuestionFrameComponent.currentCountDownTime = numberQuestionFrameComponent.maxCountDownTime;
        numberQuestionFrameComponent.SetupTexts();
    }
}
