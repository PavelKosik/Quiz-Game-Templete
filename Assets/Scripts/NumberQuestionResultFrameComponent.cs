using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberQuestionResultFrameComponent : MonoBehaviour
{
    public TMP_Text playerAnswerText;
    public TMP_Text playerTimeText;
    public TMP_Text aiAnswerText;
    public TMP_Text aiTimeText;
    [HideInInspector]
    public float playerAnswer;
    [HideInInspector]
    public float playerTime;
    [HideInInspector]
    public float aiAnswer;
    [HideInInspector]
    public float aiTime;
    public TMP_Text winnerText;
    public TMP_Text countDownText;
    public float maxCountDownTime;
    [HideInInspector]
    public float currentCountDownTime;
    // Start is called before the first frame update
    void Start()
    {
        currentCountDownTime = maxCountDownTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentCountDownTime -= Time.deltaTime;
        countDownText.text = Mathf.RoundToInt(currentCountDownTime).ToString();
        if (currentCountDownTime <= 0)
        {
            AwardWinner();
        }
    }

    public void SetupTexts()
    {
        //shows player the answers and times
        playerAnswer = Mathf.RoundToInt(playerAnswer * 100) / 100;
        playerTime = Mathf.RoundToInt(playerTime * 100) / 100;
        aiAnswer = Mathf.RoundToInt(aiAnswer * 100) / 100;
        aiTime = Mathf.RoundToInt(aiTime * 100) / 100;


        playerAnswerText.text = "Answer: " + playerAnswer;
        playerTimeText.text = "Time: " + playerTime;
        aiAnswerText.text = "Answer: " + aiAnswer;
        aiTimeText.text = "Time: " + aiTime;
        GetWinner();
    }

    void GetWinner()
    {
        //decides the winner of the question
        //the winner is decided based on who has closer to the correct answer
        //if the question can't be decided this way then the question is decided by who was faster
        NumberQuestionsTemplate question = FindObjectOfType<NumberQuestionManagerComponent>().chosenQuestion;
        float playerDiff = Mathf.Abs(playerAnswer - question.correctAnswer);
        float aiDiff = Mathf.Abs(aiAnswer - question.correctAnswer);

        if (playerDiff < aiDiff)
        {
            winnerText.text = "Player is the winner";
        }

        else if (playerDiff > aiDiff)
        {
            winnerText.text = "AI is the winner";

        }

        else
        {
            if (playerTime > aiTime)
            {
                winnerText.text = "AI is the winner";
            }

            else
            {
                winnerText.text = "Player is the winner";
            }
        }
    }

    void AwardWinner()
    {
        //handles what happens when player / AI wins and awards both parties accordingly

        if (winnerText.text == "Player is the winner")
        {
            if (FindObjectOfType<GameStageManagerComponent>().stageNumber == 0)
            {
                FindObjectOfType<PlayerComponent>().numberOfRegionsLeftToChoose = 2;
                FindObjectOfType<AIPlayerComponent>().numberOfRegionsLeftToChoose = 1;
                FindObjectOfType<AIPlayerComponent>().ChooseRegions();
                FindObjectOfType<NumberQuestionFrameComponent>().gameObject.SetActive(false);
                FindObjectOfType<AnswerManagerComponent>().shouldDisplayNumberQuestion = false;
                FindObjectOfType<AIPlayerComponent>().choseSameAnswerAsPlayer = false;
                FindObjectOfType<AttackManagerComponent>().LetPlayerChooseRegions();
                currentCountDownTime = maxCountDownTime;
                gameObject.SetActive(false);
            }

            else
            {
                FindObjectOfType<Stage1AttackManagerComponent>().playerWonQuestion = true;
                FindObjectOfType<Stage1AttackManagerComponent>().RegionAfterAttackHandle();
            }
        }

        else
        {
            if (FindObjectOfType<GameStageManagerComponent>().stageNumber == 0)
            {
                FindObjectOfType<PlayerComponent>().numberOfRegionsLeftToChoose = 1;
                FindObjectOfType<AIPlayerComponent>().numberOfRegionsLeftToChoose = 2;
                FindObjectOfType<AIPlayerComponent>().ChooseRegions();
                FindObjectOfType<NumberQuestionFrameComponent>().gameObject.SetActive(false);
                FindObjectOfType<AnswerManagerComponent>().shouldDisplayNumberQuestion = false;
                FindObjectOfType<AIPlayerComponent>().choseSameAnswerAsPlayer = false;
                FindObjectOfType<AttackManagerComponent>().LetPlayerChooseRegions();
                currentCountDownTime = maxCountDownTime;
                gameObject.SetActive(false);
            }

            else
            {
                FindObjectOfType<Stage1AttackManagerComponent>().playerWonQuestion = false;
                FindObjectOfType<Stage1AttackManagerComponent>().RegionAfterAttackHandle();
            }
        }
    }
}
