using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStageManagerComponent : MonoBehaviour
{
    private RegionManagerComponent regionManagerComponent;
    public int stageNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        regionManagerComponent = FindObjectOfType<RegionManagerComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if there are still unowned regions left
        //if that's the case then the game is still in stage 0
        //else the game enters stage 1
        if (regionManagerComponent.managedToGetRegions)
        {
            if (regionManagerComponent.unOwnedRegions.Count == 0 && stageNumber == 0)
            {
                RegionComponent[] regions = FindObjectsOfType<RegionComponent>();
                for(int i = 0; i < regions.Length; i++)
                {
                    if (regions[i].owningPlayer != "Player1" && regions[i].owningPlayer !="AIPlayer")
                    {
                        regionManagerComponent.unOwnedRegions.Add(regions[i]);
                        return;
                    }
                }

                stageNumber = 1;
            }
        }
    }
}
