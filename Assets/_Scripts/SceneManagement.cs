using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Source File Name: SceneManagement.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: October 2nd, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: November 10th, 2024
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
    public void PlayOrAgain()
    {
        //... savign logic here...  to be added in iteration 2

        ReInitializeStaticVariables();
        SceneManager.LoadScene("Game_Scene");
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
        SceneManager.LoadScene("Main_Menu");
    }



    /// <summary>
    /// Saves and loads the main menu scene
    /// </summary>
    public void SaveAndLeave()
    {
        //code for saving... to be implemented in iteration 2

        //code for  back to main
        SceneManager.LoadScene("Main_Menu");
    }


    /// <summary>
    /// Loads Open_Game_Scene with singleplayer mode chosen
    /// </summary>
    public void SingleplayerChosen()
    {
        //...set some saved prefs for singleplayer ... to be done in iteration 2
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
        //...set some saved prefs for multiplayer ... to be done in iteration 2
        ReInitializeStaticVariables();
        SceneManager.LoadScene("Open_Game_Scene_multiplayer");
        PlayerPrefs.SetInt("NumbOfPlayer", 2);
    }


    /// <summary>
    /// Deletes the player's saves
    /// </summary>
    public void DeleteSave()
    {
        //... delete save to be implemented later
    }

    /// <summary>
    /// Re-loads the game scene with the map regenerated.
    /// </summary>
    public void ContinueToNextMap()
    {
        //... regnerate map with more difficulty... to be implemented in iteration 2
        //icremenent level preferences
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
