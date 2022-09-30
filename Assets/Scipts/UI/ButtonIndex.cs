using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIndex : MonoBehaviour
{
    public int ButtonID;
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() =>UiHandlerScript.Instance.SelectAnswer());
    }

    /// <summary>
    /// Saving correct answers in List to display on result screen 
    /// </summary>
    public void SetcorrectAnswer()
    {
        if(UiHandlerScript.AnswerIndex== this.ButtonID)
        {
            UiHandlerScript.TotalCorrectAnswer++;
            UiHandlerScript.Instance.AnweredQuestion.Add(UiHandlerScript.QuestionIndex);
        }
       

    }
}
