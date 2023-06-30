using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1AttackManagerComponent : MonoBehaviour
{
    public RegionComponent regionBeingAttacked;
    public bool playerWonQuestion = false;
    public bool playerAttacking = true;
    // Start is called before the first frame update
    void Start()
    {
        playerAttacking = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RegionAfterAttackHandle()
    {
        //in stage 1 players fight for the regions
        //the attacker can win the region from the other player
        if (playerAttacking)
        {
            //if player attacked and player won he gets the region
            if (playerWonQuestion)
            {
                PlayerComponent player = FindObjectOfType<PlayerComponent>();
                regionBeingAttacked.owningPlayer = player.playerName;
                player.ownedRegions.Add(regionBeingAttacked);
                regionBeingAttacked.ChangeColor(player.playerColor);
                FindObjectOfType<AIPlayerComponent>().ownedRegions.Remove(regionBeingAttacked);
                playerAttacking = false;
                playerWonQuestion = false;
            }

            //if he attacked but didn't win then his turn is over and the other player attacks
            else
            {
                playerAttacking = false;
                playerWonQuestion = false;
            }
        }

        else
        {
            //player was the one being attacked and lost then the AI becomes the owner of the attacked region
            if(!playerWonQuestion)
            {
                AIPlayerComponent AIplayer = FindObjectOfType<AIPlayerComponent>();
                regionBeingAttacked.owningPlayer = AIplayer.aiName;
                AIplayer.ownedRegions.Add(regionBeingAttacked);
                regionBeingAttacked.ChangeColor(AIplayer.aiRegionColor);
                FindObjectOfType<PlayerComponent>().ownedRegions.Remove(regionBeingAttacked);
                playerAttacking = true;
                playerWonQuestion = false;
            }

            //else the turn ends and player can attack
            else
            {
                playerAttacking = true;
                playerWonQuestion = false;
            }
        }
    }
}
