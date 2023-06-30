using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberQuestionManagerComponent : MonoBehaviour
{
    public float playerAnswer;
    public List<NumberQuestionsTemplate> questions = new List<NumberQuestionsTemplate>();
    public NumberQuestionsTemplate chosenQuestion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //chooses a random number question to ask
    public void ChooseQuestion()
    {
        int randomIndex = Random.Range(0, questions.Count);

        chosenQuestion = questions[randomIndex];
    }
}
