using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionCountDownComponent : MonoBehaviour
{
    public float maxQuestionTime = 15.0f;
    [HideInInspector]
    public float currentQuestionTime;
    private TMP_Text countDownText;
    [HideInInspector]
    public bool shouldPlayerChooseRegions = false;
    private GameObject questionFrame;
    // Start is called before the first frame update
    void Start()
    {
        countDownText = GetComponent<TMP_Text>();
        currentQuestionTime = maxQuestionTime;
        questionFrame = FindObjectOfType<QuestionFrameComponent>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //player has limited time to answer the questions
        currentQuestionTime -= Time.deltaTime;
        countDownText.text = Mathf.RoundToInt(currentQuestionTime).ToString();

        if (currentQuestionTime <= 0.1f)
        {
            if (questionFrame.activeInHierarchy)
            {
                //if player doens't choose the answer in time one random answer gets chosen for him
                if (!FindObjectOfType<AnswerManagerComponent>().playerChoseAnswer)
                {
                   AnswerButtonComponent[] answerButtons= GameObject.FindObjectsOfType<AnswerButtonComponent>();

                    int randomIndex = Random.Range(0, answerButtons.Length);

                    answerButtons[randomIndex].ChooseAnswer();
                    currentQuestionTime = 3;
                }

                else
                {
                    FindObjectOfType<QuestionFrameComponent>().gameObject.SetActive(false);
                }
            }

        }
    }
}
