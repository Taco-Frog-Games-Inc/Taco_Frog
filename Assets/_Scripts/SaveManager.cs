/*
 * Source File Name: SaveManager.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: November 11th, 2024
 * 
 * Last Modified by: Alexander Maynard
 * Last Modified Date: November 13th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script enforces the saving of in-game elements/points.
 * 
 * Revision History:
 *      ->  November 11th, 2024:
 *          -Created starter layout for the script and completed saving for current map size.
 *      -> November 12th, 2024: 
 *          -Started saving and loading for tacos and player points (from aquired items).
 *      -> November 13th, 2024:
 *          -Cleaned up code and commented code.
 *          -Added capability to delete save data.
 *          -Added mutli and single player save and delete options
 *          -Adapted the TacoScore functions and added ResetTacoScore()
 */

using UnityEngine;

public class SaveManager : PersistGenericSingleton<SaveManager>
{
    //current tacos collected count and current score
    private int _currentTacos;
    private int _currentScore;

    private void Start()
    {
        //check to initialize map size just in case
        if (!PlayerPrefs.HasKey("mapHeight") || !PlayerPrefs.HasKey("mapLength"))
        {
            PlayerPrefs.SetInt("mapHeight", 5);
            PlayerPrefs.SetInt("mapLength", 5);
        }
        else
        {
            PlayerPrefs.SetInt("mapHeight", 5);
            PlayerPrefs.SetInt("mapLength", 5);
        }
        //reset cuurent scores
        ResetCurrentScores();
    }

    /// <summary>
    /// Increments the current taco score
    /// </summary>
    public void IncrementTacoScore() {
        _currentTacos = PlayerPrefs.GetInt("single_RecordScore");
        _currentTacos++;
        PlayerPrefs.SetInt("single_RecordScore", _currentTacos);
    }

    /// <summary>
    /// Returns the current taco score
    /// </summary>
    /// <returns>current taco score (how many tacos are collected)</returns>
    public int GetTacoScore() { return PlayerPrefs.GetInt("single_RecordScore"); }

    public void ResetTacoScore() { PlayerPrefs.SetInt("single_RecordScore", 0); }

    /// <summary>
    /// Checks if single or multiplayer, then returns the current score.
    /// </summary>
    /// <returns>Current score (based off items collected)</returns>
    public void UpdateCurrentScore()
    {
        PlayerController[] players = (PlayerController[])FindObjectsByType(typeof(PlayerController), FindObjectsSortMode.None); //get reference to all players
        
        //check if multiplayer or single: 1 --> single, 2 --> multiplayer
        if (PlayerPrefs.GetInt("NumbOfPlayer") == 1)
        {
            
            //if single player return score for player 1
            _currentScore = _currentScore + players[0].Score;
            Debug.Log("Current Score: " + _currentScore);
        }
        else
        {
            //else multiplayer so return score for player 1 and 2
            _currentScore = _currentScore + (players[0].Score + players[1].Score);
        }
    }


    /// <summary>
    /// reset current tacos count and score (items collected aggregate)
    /// </summary>
    public void ResetCurrentScores()
    {
        _currentTacos = 0;
        _currentScore = 0;
    }

    /// <summary>
    /// saves the score data (either record amount of tacos or points score).
    /// </summary>
    public void SaveData()
    {
        //check if single player
        if(PlayerPrefs.GetInt("NumbOfPlayer") == 1)
        {
            //save tacos
            if (!PlayerPrefs.HasKey("single_RecordTacos")) //if the record taco prefs are not there...
                PlayerPrefs.SetInt("single_RecordTacos", 0); //...then set to 0
            else if (_currentTacos > PlayerPrefs.GetInt("single_RecordTacos")) //otherwise if a new taco record is there...
                PlayerPrefs.SetInt("single_RecordTacos", _currentTacos); //save the new taco record

            //save score (based off of items collected)
            if (!PlayerPrefs.HasKey("single_RecordScore")) //if the score record prefs are not there...
                PlayerPrefs.SetInt("single_RecordScore", 0); //...then set to 0
            else if (_currentScore > PlayerPrefs.GetInt("single_RecordScore")) //otherwise if a new score record is there...
                PlayerPrefs.SetInt("single_RecordScore", _currentScore); //save the new score record
        }
        else //else it's multiplayer
        {
            //save tacos
            if (!PlayerPrefs.HasKey("multi_RecordTacos")) //if the record taco prefs are not there...
                PlayerPrefs.SetInt("multi_RecordTacos", 0); //...then set to 0
            else if (_currentTacos > PlayerPrefs.GetInt("multi_RecordTacos")) //otherwise if a new taco record is there...
                PlayerPrefs.SetInt("multi_RecordTacos", _currentTacos); //save the new taco record

            //save score (based off of items collected)
            if (!PlayerPrefs.HasKey("multi_RecordScore")) //if the score record prefs are not there...
                PlayerPrefs.SetInt("multi_RecordScore", 0); //...then set to 0
            else if (_currentScore > PlayerPrefs.GetInt("multi_RecordScore")) //otherwise if a new score record is there...
                PlayerPrefs.SetInt("multi_RecordScore", _currentScore); //save the new score record
        }
        //reset current scores
        ResetCurrentScores();
    }     

    /// <summary>
    /// Delete the save data for single player by setting records to 0 for taco amount and score points
    /// </summary>
    public void DeleteSinglePlayerSaveData()
    {
        PlayerPrefs.SetInt("single_RecordTacos", 0);
        PlayerPrefs.SetInt("single_RecordScore", 0);
    }

    /// <summary>
    /// Delete the save data for multiplayer by setting records to 0 for taco amount and score points
    /// </summary>
    public void DeleteMultiplayerSaveData()
    {
        PlayerPrefs.SetInt("multi_RecordTacos", 0);
        PlayerPrefs.SetInt("multi_RecordScore", 0);
    }

    /// <summary>
    /// Sets the new map size after a zone is cleared.
    /// </summary>
    public void SetNextMapSize()
    {
        if (PlayerPrefs.GetInt("mapHeight") <= 50) //limit the mapSize at a certain point.
        {
            PlayerPrefs.SetInt("mapHeight", (PlayerPrefs.GetInt("mapHeight") + 2));
            PlayerPrefs.SetInt("mapLength", (PlayerPrefs.GetInt("mapLength") + 2));
        }
    }

    /// <summary>
    /// Resets the map size after a player loses or other.
    /// </summary>
    public void ResetMapSize()
    {
        PlayerPrefs.SetInt("mapHeight", 5);
        PlayerPrefs.SetInt("mapLength", 5);
    }

    /// <summary>
    /// Loads the save data for singleplayer
    /// </summary>
    /// <returns>tuple with record tacos, and record score</returns>
    public (int, int) LoadSinglePlayerSaveData()
    {
        return (PlayerPrefs.GetInt("single_RecordTacos"), PlayerPrefs.GetInt("single_RecordScore"));
    }

    /// <summary>
    /// Loads the save data for multiplayer
    /// </summary>
    /// <returns>tuple with record tacos, and record score</returns>
    public (int, int) LoadMultiplayerSaveData()
    {
        return (PlayerPrefs.GetInt("multi_RecordTacos"), PlayerPrefs.GetInt("multi_RecordScore"));
    }
}