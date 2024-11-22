using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Source File Name: SceneManagement.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: October 2nd, 2024
 * 
 * Last Modified by: Alexander Maynard
 * Last Modified Date: November 13th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script handles the various button functionalities for menus.
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and implemented (not conunting saving 
 *          features or anything related to map generation yet):
 *              *ChangeScene(), 
 *              *QuitGame(),
 *              *PlayOrAgain(),
 *              *Options(),
 *              *LeaveOrBack(),
 *              *SaveAndLeave().
 *              *SignleplayerChosen(),
 *              *MultiplayerChosen(),
 *              *DeleteSave()
 *      -> October 3rd, 2024:
 *          -Added initial ContinueToNextMap() (no actual procedural implementation yet).
 *      -> October 14th, 2024:
 *          -Re-Initialized static variables on LoadScene
 *      -> November 10th, 2024:
 *          -Adjusted for multiplayer
 *      -> November 11th, 2024:
 *          -Adjusted buttons to incorporate the map size increasing as the player progresses.
 *          -Adjusted for testing scene.
 *      -> November 13th, 2024:
 *          -Continued to add saving capabilities for score points and taco points to be reset etc.
 *          -Added more comments.
 *          -Added mutli and single player delete options
 *          
 *
 */

public class SceneManagement : MonoBehaviour
{
    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    /// <summary>
    /// Loads the game again or for the first time.
    /// </summary>
    public void PlayGame()
    {
        SaveManager.Instance.ResetMapSize(); //reset the map size when playing (starting again or first time)
        SaveManager.Instance.ResetCurrentScores(); //reset current scores just in case

        ReInitializeStaticVariables();
        if (MenuCanvasController.isTestingScene) SceneManager.LoadScene("TestingStaticScene");
        else if (PlayerPrefs.GetInt("HasDoneTutorial") == 1) SceneManager.LoadScene("Game_Scene");
        else SceneManager.LoadScene("Tutorial");
    }

    /// <summary>
    /// Loads the options scene
    /// </summary>
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    /// <summary>
    /// Loads the main menu scene
    /// </summary>
    public void LeaveOrBack()
    {
        //don't save and call the main menu scene
        SceneManager.LoadScene("Main_Menu");
    }

    /// <summary>
    /// Saves and loads the main menu scene
    /// </summary>
    public void SaveAndLeave()
    {
        //save the point score and amount of tacos
        SaveManager.Instance.SaveData();
        //code for  back to main
        SceneManager.LoadScene("Main_Menu");
    }

    /// <summary>
    /// Loads Open_Game_Scene with singleplayer mode chosen
    /// </summary>
    public void SingleplayerChosen()
    {
        ReInitializeStaticVariables();
        SceneManager.LoadScene("Open_Game_Scene");
        PlayerPrefs.SetInt("NumbOfPlayer", 1);
    }

    /// <summary>
    /// /// <summary>
    /// Loads Open_Game_Scene with multiplayer mode chosen
    /// </summary>
    public void MultiplayerChosen()
    {
        ReInitializeStaticVariables();
        SceneManager.LoadScene("Open_Game_Scene_multiplayer");
        PlayerPrefs.SetInt("NumbOfPlayer", 2);
    }

    /// <summary>
    /// Deletes the player save for single and multi player
    /// </summary>
    public void DeletePlayerSaves()
    {
        SaveManager.Instance.DeletePlayerSaveData();
    }

    /// <summary>
    /// Re-loads the game scene with the map regenerated.
    /// </summary>
    public void ContinueToNextMap()
    {
        //... regnerate map with a bigger map by calling set next map size
        SaveManager.Instance.SetNextMapSize();
        
        ReInitializeStaticVariables();
        SceneManager.LoadScene("Game_Scene");
    }

    private void ReInitializeStaticVariables() {
        SpawnManagerABL.totalEnemyCount = 0;
        SpawnManagerABL.totalHazardsCount = 0;
        SpawnManagerABL.totalItemsCount = 0;
        NavMeshManager.isInitialize = false;
    }
}
