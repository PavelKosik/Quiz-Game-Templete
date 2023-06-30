using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberQuestionButtonComponent : MonoBehaviour
{
    private string number;
    public TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        number = GetComponentInChildren<TMP_Text>().text;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddNumber()
    {
        //adds the number this button represents to the player answer
        inputField.text = (inputField.text + number).ToString();
    }
}
