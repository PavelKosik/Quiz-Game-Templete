using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonComponent : MonoBehaviour
{
    private GameStageManagerComponent gameStageManagerComponent;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 1.0f;
        gameStageManagerComponent = FindObjectOfType<GameStageManagerComponent>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //called by the button on click
    public void ChooseRegion()
    {
        //the way player chooses regions is different based on what stage it is
        if (gameStageManagerComponent.stageNumber == 0)
        {
            //in stage 0 he can't choose the regions owned by AI player
            PlayerComponent player = FindObjectOfType<PlayerComponent>();
            if (player.numberOfRegionsLeftToChoose > 0)
            {
                //player can only choose regions he doesn't already own and that he can attack in the given turn
                RegionComponent region = GetComponentInParent<RegionComponent>();
                if (!player.ownedRegions.Contains(region) && player.canAttackRegions.Contains(region))
                {
                    //if the region fits the criteria he is added to players owned regions
                    region.owningPlayer = player.playerName;
                    player.ownedRegions.Add(region);
                    region.ChangeColor(player.playerColor);
                    player.numberOfRegionsLeftToChoose -= 1;
                    FindObjectOfType<RegionManagerComponent>().RemoveRegion(region);
                    player.GetRegionsPlayerCanAttack();
                }
            }
        }

        else
        {
            //in stage 1 player can only choose the AI player owned regions
            PlayerComponent player = FindObjectOfType<PlayerComponent>();
            RegionComponent region = GetComponentInParent<RegionComponent>();

            //player can't choose the region he already owns
            if (player.ownedRegions.Contains(region))
            {
                return;
            }

            bool isNeighbour = false;
            //only neighbour regions can be attacked
            for (int i = 0; i < player.ownedRegions.Count; i++)
            {
                if (!player.ownedRegions[i].neighbourRegionsBearSide.Contains(region))
                {
                    if (!player.ownedRegions[i].neighbourRegionsBearSide.Contains(region))
                    {
                        continue;
                    }
                }

                isNeighbour = true;
            }

            //if region is considered neighbour then question to decide the new owner of the region is displayed
            if (isNeighbour)
            {
                FindObjectOfType<Stage1AttackManagerComponent>().regionBeingAttacked = region;
                FindObjectOfType<AttackManagerComponent>().HandleQuestion();
                FindObjectOfType<AttackManagerComponent>().questionDisplayed = true;
                FindObjectOfType<AttackManagerComponent>().shouldUpdateTime = false;
            }
        }
    }
}
