using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public int GameScore = 0;
    
    private int HighScore = 0;
    private const string HighScoreKey = "high_score";
    private const string RecentScoreKey = "recent_score";

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(HighScoreKey))
        {
            HighScore = PlayerPrefs.GetInt(HighScoreKey);
        }
        else
        {
            PlayerPrefs.SetInt(HighScoreKey, GameScore);
        }
        PlayerPrefs.SetInt(RecentScoreKey, GameScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int pointValue)
    {
        GameScore += pointValue;
    }

    public void RemoveLife()
    {
        // Check to see how many lives player has
            // If player lost final life, check if high score and load game over scene
            // Otherwise, remove life and send the player back to the origin/spawn.
    }
}
