using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
   string api = "https://opentdb.com/api.php?amount=10&category=11&difficulty=easy&type=boolean";

   TrivialCs getResults;
   public QuestionCs getQuestion;

    void Awake()
    {
        // A correct website page.
        StartCoroutine( GetRequest( api ) );
    }

    void Start()
    {

    }

    IEnumerator GetRequest( string uri )
    {
        using ( UnityWebRequest wr = UnityWebRequest.Get(uri) )
        {
            // Request and wait for the desired page.
            yield return wr.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (wr.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + wr.error);
                break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + wr.error);
                break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + wr.downloadHandler.text);

                    CreateFromJSON( wr.downloadHandler.text );
                break;
            }
        }
    }

    public void CreateFromJSON(string jsonString)
    {
        getResults = JsonUtility.FromJson<TrivialCs>(jsonString);
        getQuestion = getResults.results[0];
    }
}
