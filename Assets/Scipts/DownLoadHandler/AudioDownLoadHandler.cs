using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
 
public class AudioDownLoadHandler : MonoBehaviour
{
     
    AudioSource audioSource;
    AudioClip myClip;

    public static AudioDownLoadHandler Instance;
    Coroutine newCLip;
    Coroutine newImage;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
       
    }

    public void PlayAudioClip(string path)
    {
        newCLip = StartCoroutine(GetAudioClip(path));
    }
    public void SetImageforSong(string path)
    {
        newImage = StartCoroutine(LoadImageFromURL(path));
    }
    /// <summary>
    /// Downloading Audio file from server
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    
    IEnumerator GetAudioClip(string path)
    { 
        
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV))
        {
            yield return www.SendWebRequest();
 
            if (www.result==UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
                if (newCLip != null)
                    StopCoroutine(newCLip);
            }
            else
            {
                myClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = myClip;
                audioSource.Play();
                Debug.Log("Audio is playing.");
                if (newCLip != null)
                    StopCoroutine(newCLip);
            }
        }
    }
    /// <summary>
    /// Downloading image file from server
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    IEnumerator LoadImageFromURL(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(www.error);
            if (newImage != null)
                StopCoroutine(newImage);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
            UiHandlerScript.Instance.SongImage.sprite = sprite;
            if (newImage != null)
                StopCoroutine(newImage);
        }
    }


    public void pauseAudio(){
        audioSource.Pause();
    }
     
    public void playAudio(){
        audioSource.Play();
    }
     
    public void stopAudio()
    {
        if (newCLip != null)
            StopCoroutine(newCLip);
        audioSource.Stop();
     
    }
}