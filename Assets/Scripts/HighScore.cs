using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class HighScore : MonoBehaviour
{
    [SerializeField]
    TMP_Text playerScoreDisplay; // The display for the player's current score
    [SerializeField]
    TMP_Text highscoreDisplay; // The display for the top scores

    public string playerName = "Default";
    public int playerScore = 0;

    [SerializeField]
    string privateKey = "gkzNnM9vI0Wgd5uY4UhyxgPOlYOW-ceka8naa-zWwwPA";
    [SerializeField]
    string publicKey = "6050d5d8778d3d7a300659af";

    public void ResetScore()
    {
        playerScore = 0;
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    public void IncreaseScore(int amount)
    {
        playerScore += amount;
        playerScoreDisplay.text = "Score: " + playerScore; // Update the player's score display
    }

    // This will add a new high score to the list
    public void Add()
    {
        StartCoroutine(
            HTTPGet("http://dreamlo.com/lb/" + privateKey + "/add/" + playerName + "/" + playerScore)
        );
    }

    // Unfortunately, coroutines cannot be started directly from unity events
    public void UpdateScoreDisplay()
    {
        StartCoroutine(LoadScores());
    }

    // This will return all high scores as a string
    public IEnumerator LoadScores()
    {
        using (UnityWebRequest www = new UnityWebRequest("http://dreamlo.com/lb/" + publicKey + "/pipe"))
        {
            www.downloadHandler = new DownloadHandlerBuffer(); // Required for accessing the contents of the page

            highscoreDisplay.text = "LOADING...";
            // Request the page, and only continue once it is received
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Failed to access web service");
                highscoreDisplay.text = "Failed to load scores";
            }
            else
            {
                highscoreDisplay.text = "";

                string[] scores = www.downloadHandler.text.Split('\n');

                for(int i = 0; i < scores.Length; i += 1)
                {
                    string[] currentScore = scores[i].Split('|');
                    if (currentScore.Length < 2) continue; // Ensure that valid data is available on each line
                    // Append the score the the display text
                    highscoreDisplay.text += currentScore[0] + ": " + currentScore[1] + "\n";
                }
            }
        }
    }

    // This will simply send a HTTP get request, used to update dreamlo scores
    IEnumerator HTTPGet(string url)
    {
        using (UnityWebRequest www = new UnityWebRequest(url))
        {
            // Request the page, and only continue once it is received
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log("Failed to access web service");
            }
        }
    }
}
