using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegionComponent : MonoBehaviour
{
    public int regionIndex;
    public bool isBullSide;
    public string owningPlayer = "null";
    public List<RegionComponent> neighbourRegionsBullSide = new List<RegionComponent>();
    public List<RegionComponent> neighbourRegionsBearSide = new List<RegionComponent>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor(Color playerColor)
    {
        //changes color based on who the owning player is
        GetComponentInChildren<Image>().color = playerColor;
    }
}
