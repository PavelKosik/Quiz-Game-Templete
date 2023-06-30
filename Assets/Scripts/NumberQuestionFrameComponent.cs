using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberQuestionFrameComponent : MonoBehaviour
{
    public TMP_Text headingText;
    public float playerAnswerTime;
    // Start is called before the first frame update
    void Start()
    {
        playerAnswerTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //player answers time increases because in this question type the speed can be a deciding factor for who the winner is
        playerAnswerTime += Time.deltaTime;
    }

    public void SetupText(NumberQuestionManagerComponent numberQuestionManagerComponent)
    {
        //setups the texts of the question
        GetComponentInChildren<TMP_InputField>().text = "";
        headingText.text = numberQuestionManagerComponent.chosenQuestion.questionHeading;
        playerAnswerTime = 0;
    }
}
