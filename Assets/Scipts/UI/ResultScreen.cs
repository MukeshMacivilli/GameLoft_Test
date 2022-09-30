using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultScreen : MonoBehaviour
{

    public Text ResultScreenText;
    public GameObject AnswerdisplayPanel;
    public GameObject AnswerwDisplayParentPanel;

    // Start is called before the first frame update
    void OnEnable()
    {
        CretateAnswerList();
    }

    /// <summary>
    /// Resting game to the new level or coming back to play list screen
    /// </summary>
    public void ResteGame()
    {
        UiHandlerScript.QuestionIndex = 0;
        UiHandlerScript.TotalCorrectAnswer = 0;
        UiHandlerScript.Instance.MyGamestate = GameState.PlayList;
        ReadJsonData.Instance.CorrectAnswers.Clear();
        UiHandlerScript.Instance.AnweredQuestion.Clear();
        UiHandlerScript.Instance.EnableGameScreens(UiHandlerScript.Instance.MyGamestate);
        foreach (Transform child in AnswerwDisplayParentPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void CretateAnswerList()
    {
        ResultScreenText.text = UiHandlerScript.TotalCorrectAnswer + "/" + UiHandlerScript.TotalQuestions;
        CreateAnswerLabels(UiHandlerScript.TotalQuestions);
    }
    /// <summary>
    /// Displaying Correct answer on the result screen
    /// </summary>
    /// <param name="totalQiestions"></param>
    void CreateAnswerLabels(int totalQiestions)
    {
        for(int i=0;i< totalQiestions;i++)
        {
            GameObject panel = Instantiate(AnswerdisplayPanel) as GameObject;
            panel.transform.GetChild(2).transform.gameObject.SetActive(true);
            panel.GetComponentInChildren<Text>().text =" "+(i+1)+" : "+ ReadJsonData.Instance.CorrectAnswers[i];
            panel.transform.parent = AnswerwDisplayParentPanel.transform;

            for(int j=0;j<UiHandlerScript.Instance.AnweredQuestion.Count;j++)
            {
                if(i== UiHandlerScript.Instance.AnweredQuestion[j])
                {
                    panel.transform.GetChild(2).transform.gameObject.SetActive(false);
                }
            }
        }

    }
}
