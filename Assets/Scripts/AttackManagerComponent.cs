using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackManagerComponent : MonoBehaviour
{
    [HideInInspector]
    public PlayerComponent player;
    public GameObject questionsFrame;
    public float timeToWaitAfterSideChoose = 5.0f;
    private float currentWaitTime = 0.0f;
    [HideInInspector]
    public bool questionDisplayed = false;
    public bool playerChoseSide = false;
    public TMP_Text instructionText;
    [HideInInspector]
    public bool shouldUpdateTime = false;
    private string instruction = "Prepare to answer the upcoming question...";
    public QuestionCountDownComponent questionCountDownComponent;
    private GameStageManagerComponent gameStageManagerComponent;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerComponent>();
        currentWaitTime = timeToWaitAfterSideChoose;
        instruction = "Prepare to answer the upcoming question...";
        gameStageManagerComponent = FindObjectOfType<GameStageManagerComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        //once player chooses side the countdown starts to star the questions
        if (playerChoseSide)
        {
            if (!questionDisplayed)
            {
                shouldUpdateTime = true;
            }
        }

        if (shouldUpdateTime)
        {
            currentWaitTime -= Time.deltaTime;

            instructionText.text = instruction + Mathf.RoundToInt(currentWaitTime).ToString();

            if (currentWaitTime <= 0)
            {
                //after the timer runs out the question is displayed
                HandleQuestion();
                questionDisplayed = true;
                shouldUpdateTime = false;
            }
        }

        if (gameStageManagerComponent.stageNumber == 0)
        {
            //after the questions were handled player can choose a region
            if (instruction == "Choose a region...")
            {
                //after player chooses all the regions he could that turn then timer for next question starts
                if (player.numberOfRegionsLeftToChoose == 0)
                {
                    instruction = "Prepare to answer the upcoming question...";
                    currentWaitTime = 5.0f;
                }

                else
                {
                    //else player has a timer to choose the regions
                    //if he can't choose the regions until the timer runs out then the rergions are chosen for him randomly
                    if (currentWaitTime <= 0.1f)
                    {
                        player.GetRegionsPlayerCanAttack();


                        int randomIndex = Random.Range(0, player.canAttackRegions.Count);
                        player.canAttackRegions[randomIndex].owningPlayer = player.playerName;
                        player.canAttackRegions[randomIndex].ChangeColor(player.playerColor);
                        player.ownedRegions.Add(player.canAttackRegions[randomIndex]);
                        FindObjectOfType<RegionManagerComponent>().RemoveRegion(player.canAttackRegions[randomIndex]);
                        player.numberOfRegionsLeftToChoose--;
                    }
                }
            }
        }

        else
        {
            instruction = "Select the region you wish to attack...";
        }
    }

    public void HandleQuestion()
    {
        //makes sure that the question is active and sets it up accordingly
        questionCountDownComponent.currentQuestionTime = questionCountDownComponent.maxQuestionTime;
        FindObjectOfType<AnswerManagerComponent>().playerChoseAnswer = false;
        questionsFrame.SetActive(true);
        QuestionsManagerComponent questionsManager = FindObjectOfType<QuestionsManagerComponent>();
        questionsManager.ChooseQuestion();
        questionsFrame.GetComponent<QuestionFrameComponent>().SetupTexts(questionsManager);
    }

    public void LetPlayerChooseRegions()
    {
        //allows player to choose a region
        player.GetRegionsPlayerCanAttack();
        currentWaitTime = 15;
        shouldUpdateTime = true;
        instruction = "Choose a region...";
    }

}
