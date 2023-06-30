using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionManagerComponent : MonoBehaviour
{

    public List<RegionComponent> unOwnedRegions = new List<RegionComponent>();
    [HideInInspector]
    public bool managedToGetRegions = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RegionSetup();
    }

    void RegionSetup()
    {
        //checks if there are still regions no player owns
        if (!managedToGetRegions)
        {
            unOwnedRegions.AddRange(FindObjectsOfType<RegionComponent>());

            if (unOwnedRegions.Count > 0)
            {
                managedToGetRegions = true;
            }
        }
    }

    public void RemoveRegion(RegionComponent region)
    {
        //removes region from the regions nobody owns
        if (managedToGetRegions)
        {
            int removeIndex = unOwnedRegions.IndexOf(region);
            if (removeIndex>=0) {
                unOwnedRegions.RemoveAt(removeIndex);
            }
        }

        else
        {
            RegionSetup();
            RemoveRegion(region);
        }
    }


}
