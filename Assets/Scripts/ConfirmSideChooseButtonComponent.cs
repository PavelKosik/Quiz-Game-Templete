using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmSideChooseButtonComponent : MonoBehaviour
{
    public string chosenSide;
    public GameObject chooseSideBackground;
    public GameObject gameBackground;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Confirm()
    {
        //confirms which side player chose and sets the AI as the other side
        //setups both sides accordingly
        if (chosenSide != "")
        {
            gameBackground.SetActive(true);
            PlayerComponent player = FindObjectOfType<PlayerComponent>();
            player.isBullSide = (chosenSide == "Bull") ? true : false;
            player.startRegionIndex = (chosenSide == "Bull") ? 12 : 10;
            player.playerColor = (chosenSide == "Bull") ? player.bullColor : player.bearColor;
            player.OwnStartRegion();
            AIPlayerComponent ai = FindObjectOfType<AIPlayerComponent>();
            ai.SetupAIPlayer();
            ai.OwnStartRegion();
            chooseSideBackground.SetActive(false);           
            FindObjectOfType<AttackManagerComponent>().playerChoseSide = true;
        }
    }
}
