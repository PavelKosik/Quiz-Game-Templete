using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButtonComponent : MonoBehaviour
{
    public bool isCorrectAnswer = false;
    public NumberQuestionFrameComponent numberQuestionFrameComponent;
    private GameStageManagerComponent gameStageManagerComponent;
    // Start is called before the first frame update
    void Start()
    {
        gameStageManagerComponent = FindObjectOfType<GameStageManagerComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<AnswerManagerComponent>().shouldDisplayNumberQuestion)
        {
            if (!FindObjectOfType<AnswerManagerComponent>().numberQuestionFrame.activeInHierarchy)
            {
                if (FindObjectOfType<QuestionCountDownComponent>().currentQuestionTime <= 1)
                {
                    //chooses and displays the number question 
                    FindObjectOfType<NumberQuestionManagerComponent>().ChooseQuestion();
                    numberQuestionFrameComponent.SetupText(FindObjectOfType<NumberQuestionManagerComponent>());
                    FindObjectOfType<AnswerManagerComponent>().numberQuestionFrame.SetActive(true);
                }
            }
        }
    }

    public void ChooseAnswer()
    {
        //if player once chooses answer he cannot change it
        if (FindObjectOfType<AnswerManagerComponent>().playerChoseAnswer)
        {
            return;
        }

        FindObjectOfType<AnswerManagerComponent>().playerChoseAnswer = true;

        //the answer's background gets changed to player color to show which answer player chose
        GetComponent<Image>().material = null;
        GetComponent<Image>().color = FindObjectOfType<PlayerComponent>().playerColor;

        //then the same is done for the answer of the AI player
        AIPlayerComponent ai = FindObjectOfType<AIPlayerComponent>();
        ai.HighlightAnswer(this);

        //makes player see the answer AI chose for at least 3.0 seconds
        QuestionCountDownComponent questionCountDown = FindObjectOfType<QuestionCountDownComponent>();

        if (questionCountDown.currentQuestionTime > 3.0f)
        {
            questionCountDown.currentQuestionTime = 3.0f;
        }

        //players answer was correct
        if (isCorrectAnswer)
        {
            //if both player and AI are correct then the number question is displayed to decide who the winner is
            if (ai.choseSameAnswerAsPlayer)
            {
                FindObjectOfType<AnswerManagerComponent>().shouldDisplayNumberQuestion = true;
            }

            //if AI chose different answer from player then the AI must be wrong
            else
            {
                //if the stage is 0 then player gets to choose 2 regions and AI gets to choose 1
                if (gameStageManagerComponent.stageNumber == 0)
                {
                    FindObjectOfType<PlayerComponent>().numberOfRegionsLeftToChoose = 2;
                    FindObjectOfType<AttackManagerComponent>().LetPlayerChooseRegions();
                    ai.numberOfRegionsLeftToChoose = 1;
                    ai.ChooseRegions();
                }

                //else player becomes the owner of the region that was attacked
                else
                {
                    FindObjectOfType<Stage1AttackManagerComponent>().playerWonQuestion = true;
                    FindObjectOfType<Stage1AttackManagerComponent>().RegionAfterAttackHandle();
                }
            }
        }

        //if player was wrong
        else
        {
            //if the player answer was wrong and the stage is 0 player gets to choose 1 region

            if (gameStageManagerComponent.stageNumber == 0)
            {

                FindObjectOfType<PlayerComponent>().numberOfRegionsLeftToChoose = 1;
                FindObjectOfType<AttackManagerComponent>().LetPlayerChooseRegions();

                //if AI player chose the correct answer he gets to choose 2 regions
                if (ai.chosenAnswer.isCorrectAnswer)
                {
                    ai.numberOfRegionsLeftToChoose = 2;
                }
                //else he gets to choose only 1
                else
                {
                    ai.numberOfRegionsLeftToChoose = 1;
                }
                FindObjectOfType<AIPlayerComponent>().ChooseRegions();
            }
        }
        //resets the varibles so they don't affect another question
        ai.choseSameAnswerAsPlayer = false;
        isCorrectAnswer = false;
    }
}
