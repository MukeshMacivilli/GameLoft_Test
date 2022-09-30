using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    PlayList,
    Quiz,
    Result,
}

public class UiHandlerScript : MonoBehaviour
{
    public static UiHandlerScript Instance;

    public GameState MyGamestate = GameState.PlayList;

    [Header("PlayListScreen")]
    public GameObject PlaylistScreen;
    public Button PlayListButton;
    public GameObject ButtonPanel;

    [Header("QuizScreen")]
    [Space(10)]
    public GameObject QuizScreen;
    public List<GameObject> OptionButtons;
    public Image SongImage;

    [Header("ResultScreen")]
    [Space(10)]
    public GameObject ResultScreen;
    public List<int> AnweredQuestion;



    public static int QuestionIndex;
    public static int AnswerIndex;
    public static int TotalQuestions;
    public static int TotalCorrectAnswer;
    public static string CurrentPlayList = "";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        QuestionIndex = 0;
        EnableGameScreens(MyGamestate);
    }

    /// <summary>
    /// Creating playlist button 
    /// </summary>
    /// <param name="name"></param>
    public void SetPlayListButton(string name)
    {

            Button button = Instantiate(PlayListButton);
            button.transform.SetParent(ButtonPanel.transform);
            button.GetComponentInChildren<Text>().text = name;
            button.GetComponent<Button>().onClick.AddListener(() => PlayListButtonClick(name));
            
    }
    /// <summary>
    /// Button event for playList Button
    /// </summary>
    /// <param name="playList"></param>
    public void PlayListButtonClick(string playList)
    {
        if(MyGamestate==GameState.PlayList)
        {
            MyGamestate = GameState.Quiz;
        }
        EnableGameScreens(MyGamestate);
        Invoke("ChangeQuestion", 0f);
        CurrentPlayList = playList;
    }
    /// <summary>
    /// Enable screen function accorning to the game state
    /// </summary>
    /// <param name="newGamestate"></param>
    public void EnableGameScreens(GameState newGamestate)
    {
        DisableAllScreens();
        switch(newGamestate)
        {
            case GameState.PlayList:
                PlaylistScreen.SetActive(true);
                break;
            case GameState.Quiz:
                QuizScreen.SetActive(true);
                break;
            case GameState.Result:
                ResetGame();
                break;
        }
        
    }

    void DisableAllScreens()
    {
        PlaylistScreen.SetActive(false);
        QuizScreen.SetActive(false);
        ResultScreen.SetActive(false);
    }
    /// <summary>
    /// setting answers in options buttons
    /// </summary>
    /// <param name="name"></param>
    /// <param name="optionNo"></param>
    public void SetOptionsInButton(string name,int optionNo)
    {
        OptionButtons[optionNo].GetComponentInChildren<Text>().text =" "+ (optionNo+1)+" : "+name;    

    }
    /// <summary>
    /// Answerbutton event 
    /// </summary>
    public void SelectAnswer()
    {
       
        AudioDownLoadHandler.Instance.stopAudio();
    
        ColorChangeButtton(AnswerIndex);

        if (QuestionIndex<TotalQuestions-1)
        {
            QuestionIndex++;
            Invoke("ChangeQuestion", 1.5f);
           
        }   
        else
        {
            
            if (MyGamestate == GameState.Quiz)
            {
                MyGamestate = GameState.Result;
            }

            Invoke("LoadingResultscreen", 1.5f);

        }

        
    }

    /// <summary>
    /// Change the color for the buttons according to the user inputs
    /// </summary>
    /// <param name="answerindex"></param>
    void ColorChangeButtton(int answerindex)
    {
        foreach(var button in OptionButtons)
        {
            if (button.GetComponent<ButtonIndex>().ButtonID== answerindex)
            {
                var colors = button.GetComponent<Button>().colors;
                colors.normalColor = Color.green;
                colors.selectedColor = Color.green;
                button. GetComponent<Button>().colors = colors;
               
            }
            else
            {
                var colors = button.GetComponent<Button>().colors;
                colors.normalColor = Color.red;
                colors.selectedColor = Color.red;
                button.GetComponent<Button>().colors = colors;
               
            }

        }
    }
    /// <summary>
    /// Question changing after each question complete
    /// </summary>
    void ChangeQuestion()
    {
        foreach (var button in OptionButtons)
        {
            var colors = button.GetComponent<Button>().colors;
            colors.normalColor = Color.white;
            colors.selectedColor = Color.white;
            button.GetComponent<Button>().colors = colors;
        }
        ReadJsonData.Instance.SetQuestions(CurrentPlayList, QuestionIndex);
    }

    private void ResetGame()
    {      
        ResultScreen.SetActive(true);     
    }
    private void LoadingResultscreen()
    {
        EnableGameScreens(MyGamestate);
    }

}
