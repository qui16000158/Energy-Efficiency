using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class WebDialogue : MonoBehaviour
{
    [SerializeField]
    DialogueHandler handler;
    [SerializeField]
    string url;

    // Start is called before the first frame update (A coroutine is used in place of the usual method for web access)
    IEnumerator Start()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            // Attach a download handler so we can download our file
            www.downloadHandler = new DownloadHandlerBuffer();

            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Failed to connect to web service");
            }
            else
            {
                string text = www.downloadHandler.text; // Access text from downloaded file

                string[] listOfDialogue = text.Split('\n');

                if(listOfDialogue.Length > 0)
                {
                    handler.dialogueList = new List<Dialogue>();
                    Dialogue dialogue = new Dialogue();

                    foreach (string currentLine in listOfDialogue)
                    {
                        dialogue.conversation.Add(currentLine);
                    }

                    dialogue.onDialogueEnded = new UnityEvent();

                    // Set the dialogue as inactive at the end of the conversation
                    dialogue.onDialogueEnded.AddListener(() =>
                    {
                        handler.transform.parent.gameObject.SetActive(false);
                    });

                    handler.dialogueList.Add(dialogue);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
