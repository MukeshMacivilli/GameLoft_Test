using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ReadJsonData : MonoBehaviour
{

    public static ReadJsonData Instance;
 
    public List<QuestionData> PlayListQuestions;
    public List<Choice> AnswerOptions;
    public List<string> CorrectAnswers;
   
    JsonData myData;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
      

    }

    private void Start()
    {
        LoadJsonData();
    }

    /// <summary>
    /// Reading Json data from the saved path
    /// </summary>
    private void LoadJsonData()
    {
        string jsonPath = "" + Application.dataPath + "/Data/Questions.json";
        string myJsonResponse = File.ReadAllText(jsonPath);
        try
        {
            myData = JsonUtility.FromJson<JsonData>(myJsonResponse);
        }
        catch
        {
            Debug.Log("Loading data failed" + myJsonResponse);
        }

        foreach (var playlist in myData.Data)
        {
          
            if (UiHandlerScript.Instance != null)
                UiHandlerScript.Instance.SetPlayListButton(playlist.playlist);

            PlayListQuestions.Add(playlist);

        }


    }

    /// <summary>
    /// Setting and assignment questions and aswer acording to the data from json file
    /// </summary>
    /// <param name="playListName"></param>
    /// <param name="questionIndex"></param>

    public void SetQuestions(string playListName,int questionIndex)
    {
        string playListURL = "";
        string imageURL = "";
        foreach (var playlist in PlayListQuestions)
        {
            if (playlist.playlist == playListName)
            {
                UiHandlerScript.TotalQuestions = playlist.questions.Count;
                if (questionIndex<playlist.questions.Count)
                {
                    playListURL = playlist.questions[questionIndex].song.sample;
                    imageURL = playlist.questions[questionIndex].song.picture;
                    AnswerOptions = playlist.questions[questionIndex].choices;
                    UiHandlerScript.AnswerIndex = playlist.questions[questionIndex].answerIndex;
                    var correctAnwser = AnswerOptions[UiHandlerScript.AnswerIndex];
                    CorrectAnswers.Add("Artist: " + correctAnwser.artist + " , " + correctAnwser.title);
                    int index = 0;
                    foreach (var answer in AnswerOptions)
                    {
                        string myAnswer = "Artist : " + answer.artist + "," + "  Title : " + answer.title;
                        UiHandlerScript.Instance.SetOptionsInButton(myAnswer, index);
                        
                        index++;
                    }
                } 
            }
        }
        AudioDownLoadHandler.Instance.PlayAudioClip(playListURL);
        AudioDownLoadHandler.Instance.SetImageforSong(imageURL);

    }
}

[System.Serializable]
public class JsonData
{
    public List<QuestionData> Data;
}

   
[System.Serializable]
public class Choice
{
    public string artist;
    public string title;
}

[System.Serializable]
public class Question
{
    public string id;
    public int answerIndex;
    public List<Choice> choices;
    public Song song;
    public string type;
}

[System.Serializable]
public class QuestionData
{
    public string id;
    public List<Question> questions;
    public string playlist;
}

[System.Serializable]
public class Song
{
    public string id;
    public string title;
    public string artist;
    public string picture;
    public string sample;
}




