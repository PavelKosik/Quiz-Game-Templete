using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsManagerComponent : MonoBehaviour
{
    public List<QuestionsTemplate> questions = new List<QuestionsTemplate>();
    public QuestionsTemplate chosenQuestion;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //chooses a random question to ask
    public void ChooseQuestion()
    {
        int randomIndex = Random.Range(0, questions.Count);

        chosenQuestion = questions[randomIndex];
    }
}


