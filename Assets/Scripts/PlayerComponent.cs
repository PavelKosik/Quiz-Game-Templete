using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    public int startRegionIndex;
    public bool isBullSide;
    public string playerName;
    public bool isPlayerTurn;
    public List<RegionComponent> ownedRegions;
    public List<RegionComponent> canAttackRegions = new List<RegionComponent>();
    public Color bullColor;
    public Color bearColor;
    public Color playerColor;
    public int numberOfRegionsLeftToChoose;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetRegionsPlayerCanAttack()
    {
        //makes sure the list isn't filled with regions player can no longer attack
        canAttackRegions.Clear();

        //searches for neighbour regions of the regions player already owns
        //those will be the regions player can attack that turn
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

        //if there are no more neighbour regions player can attack then he can attack every other region
        if (canAttackRegions.Count == 0)
        {
            canAttackRegions = FindObjectOfType<RegionManagerComponent>().unOwnedRegions;
        }
    }

    public void OwnStartRegion()
    {
        //gets the start region to the player so that he can have neighbour regions to attack
        RegionComponent[] regions = FindObjectsOfType<RegionComponent>();

        for (int i = 0; i < regions.Length; i++)
        {
            if (regions[i].isBullSide == isBullSide && regions[i].regionIndex == startRegionIndex)
            {
                regions[i].owningPlayer = playerName;
                ownedRegions.Add(regions[i]);
                regions[i].ChangeColor(playerColor);
                FindObjectOfType<RegionManagerComponent>().RemoveRegion(regions[i]);
            }
        }
    }
}
