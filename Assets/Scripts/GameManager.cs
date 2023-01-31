using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const string api = "https://opentdb.com/api.php?amount=10&category=11&difficulty=easy&type=boolean";
    void Start()
    {
        // A correct website page.
        StartCoroutine( GetRequest( api ) );

        // A non-existing page.
        // StartCoroutine(GetRequest("https://error.html"));
    }

    IEnumerator GetRequest( string uri )
    {
        using ( UnityWebRequest webRequest = UnityWebRequest.Get(uri) )
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
}
