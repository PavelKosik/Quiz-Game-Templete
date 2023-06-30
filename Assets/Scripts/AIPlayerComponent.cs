using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AIPlayerComponent : MonoBehaviour
{
    public string aiName;
    private PlayerComponent player;
    private bool isBullSide = false;
    [HideInInspector]
    public Color aiRegionColor;
    private int startRegionIndex;
    [HideInInspector]
    public List<RegionComponent> ownedRegions = new List<RegionComponent>();
    private List<RegionComponent> canAttackRegions = new List<RegionComponent>();
    [HideInInspector]
    public AnswerButtonComponent chosenAnswer;
    public Material gradientMat;
    [HideInInspector]
    public bool choseSameAnswerAsPlayer = false;
    public int numberOfRegionsLeftToChoose = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerComponent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetupAIPlayer()
    {
        //sets the proper starting values for AI player based on which side player chose to play as
        isBullSide = !player.isBullSide;
        aiRegionColor = (isBullSide) ? player.bullColor : player.bearColor;
        startRegionIndex = (isBullSide) ? 12 : 10;
    }

    public void OwnStartRegion()
    {
        //owns the start region for AI the same way it does to the player
        RegionComponent[] regions = FindObjectsOfType<RegionComponent>();

        for (int i = 0; i < regions.Length; i++)
        {
            if (regions[i].isBullSide == isBullSide && regions[i].regionIndex == startRegionIndex)
            {
                regions[i].owningPlayer = aiName;
                ownedRegions.Add(regions[i]);
                regions[i].ChangeColor(aiRegionColor);
                FindObjectOfType<RegionManagerComponent>().RemoveRegion(regions[i]);
            }
        }
    }

    public void ChooseAnswer()
    {
        //AI always chooses a random aswer
        QuestionFrameComponent questionFrameComponent = FindObjectOfType<QuestionFrameComponent>();
        int index = Random.Range(0, questionFrameComponent.answerTexts.Length);
        chosenAnswer = questionFrameComponent.answerTexts[index].GetComponentInParent<AnswerButtonComponent>();
    }

    public void HighlightAnswer(AnswerButtonComponent playerAnswer)
    {
        //if player and AI chose different answer
        //then the answer's background is set to AI color to reflect which answer AI chose
        if (chosenAnswer != playerAnswer)
        {
            chosenAnswer.GetComponent<Image>().material = null;
            chosenAnswer.GetComponent<Image>().color = aiRegionColor;
        }

        //else the answer's color is 50/50 to show that both players chose that answer
        else
        {
            choseSameAnswerAsPlayer = true;
            chosenAnswer.GetComponent<Image>().material = gradientMat;
        }
    }

    public void ChooseRegions()
    {
        if (FindObjectOfType<GameStageManagerComponent>().stageNumber == 0)
        {
            //the regions AI player chooses are random
            //but the same rules to choosing them apply as with the player
            while (numberOfRegionsLeftToChoose > 0)
            {

                GetRegionsPlayerCanAttack();
                if (canAttackRegions.Count <= 0)
                {
                    canAttackRegions = FindObjectOfType<RegionManagerComponent>().unOwnedRegions;
                }

                int indexOfRegion = Random.Range(0, canAttackRegions.Count);

                if (ownedRegions.Contains(canAttackRegions[indexOfRegion]))
                {
                    canAttackRegions.RemoveAt(indexOfRegion);
                    continue;
                }

                canAttackRegions[indexOfRegion].owningPlayer = aiName;
                ownedRegions.Add(canAttackRegions[indexOfRegion]);
                canAttackRegions[indexOfRegion].ChangeColor(aiRegionColor);
                FindObjectOfType<RegionManagerComponent>().RemoveRegion(canAttackRegions[indexOfRegion]);
                numberOfRegionsLeftToChoose--;
            }

            FindObjectOfType<PlayerComponent>().GetRegionsPlayerCanAttack();
        }
    }

    public void GetRegionsPlayerCanAttack()
    {
        canAttackRegions.Clear();

        //gets the regions AI can attack based on which regions are considered neighbours
        for (int i = 0; i < ownedRegions.Count; i++)
        {
            for (int a = 0; a < ownedRegions[i].neighbourRegionsBearSide.Count; a++)
            {
                if (ownedRegions[i].neighbourRegionsBearSide[a].owningPlayer == "null")
                {
                    canAttackRegions.Add(ownedRegions[i].neighbourRegionsBearSide[a]);
                }
            }

            for (int a = 0; a < ownedRegions[i].neighbourRegionsBullSide.Count; a++)
            {
                if (ownedRegions[i].neighbourRegionsBullSide[a].owningPlayer == "null")
                {
                    canAttackRegions.Add(ownedRegions[i].neighbourRegionsBullSide[a]);
                }
            }
        }
    }
}
