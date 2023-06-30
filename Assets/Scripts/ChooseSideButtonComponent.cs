using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseSideButtonComponent : MonoBehaviour
{
    public string sideName;
    // Start is called before the first frame update
    void Start()
    {
        //alows hover effect for the side image
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //allows player to choose which side he would like to play as
    public void ChooseSide()
    {
        FindObjectOfType<ConfirmSideChooseButtonComponent>().chosenSide = sideName;
    }
}
