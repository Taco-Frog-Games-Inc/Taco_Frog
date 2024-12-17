using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Source File Name: SceneManagement.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: October 2nd, 2024
 * 
 * Last Modified by: Audre Bernier Larose
 * Last Modified Date: December 5th, 2024
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
 *      -> November 24th, 2024:
 *          -Added the static SceneManagementUtils inner class.
 *          -Adjusted for tutorial navigation
 *      -> November 30th, 2024:
 *          -Added scene change fade
 *      -> December 4th, 2024:
 *          -Make sure timeScale is set properly for pause menu when leaving the scene.
 *      -> December 5th, 2024:
 *          -Adjusted for difficulty
 */

public class SceneManagement : MonoBehaviour
{
    public static Difficulty difficultyLevel;

    [SerializeField] private Button easyBtn;
    [SerializeField] private Button mediumBtn;
    [SerializeField] private Button hardBtn;
    [SerializeField] private SpawnManagerABL manager;

    private void Start() {
        easyBtn?.onClick.AddListener(delegate { OnDifficultySelected(Difficulty.Easy); });
        mediumBtn?.onClick.AddListener(delegate { OnDifficultySelected(Difficulty.Medium); });
        hardBtn?.onClick.AddListener(delegate { OnDifficultySelected(Difficulty.Hard); });
    }

    private void OnDifficultySelected(Difficulty difficulty) {
        difficultyLevel = difficulty;
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
        PlayerPrefs.SetInt("HasDoneTutorial", 0); //Resets the tutorial condition everytime a player quits.
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

        SceneChangeFade.Instance.AddSceneFade();

        if (MenuCanvasController.isTestingScene) SceneManager.LoadScene("TestingStaticScene");
        else if (PlayerPrefs.GetInt("HasDoneTutorial") == 1) SceneManager.LoadScene("Game_Scene");
        else SceneManager.LoadScene("Tutorial");
    }

    /// <summary>
    /// Loads the options scene
    /// </summary>
    public void Options()
    {
        SceneChangeFade.Instance.AddSceneFade(); //scene transition fade
        SceneManager.LoadScene("Options");
    }

    /// <summary>
    /// Loads the main menu scene
    /// </summary>
    public void LeaveOrBack()
    {
        Time.timeScale = 1.0f; //make sure timescale is still 1 for the pause menu

        SceneChangeFade.Instance.AddSceneFade(); //scene transition fade

        //don't save and call the main menu scene
        SceneManager.LoadScene("Main_Menu");
    }

    /// <summary>
    /// Saves and loads the main menu scene
    /// </summary>
    public void SaveAndLeave()
    {
        Time.timeScale = 1.0f; //make sure timescale is still 1 for the pause menu
        SceneChangeFade.Instance.AddSceneFade(); //scene transition fade

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
        SceneChangeFade.Instance.AddSceneFade(); //scene transition fade

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
        SceneChangeFade.Instance.AddSceneFade(); //scene transition fade

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

        SceneChangeFade.Instance.AddSceneFade(); //scene transition fade

        SceneManager.LoadScene("Game_Scene");
    }

    public void OnStoreSelected(bool isStore) {
        gameObject.transform.GetChild(3).gameObject.SetActive(!isStore);
        gameObject.transform.GetChild(4).gameObject.SetActive(isStore);
    }

    private void ReInitializeStaticVariables() {
        SpawnManagerABL.totalEnemyCount = 0;
        SpawnManagerABL.totalHazardsCount = 0;
        SpawnManagerABL.totalItemsCount = 0;
        NavMeshManager.isInitialize = false;
    }

    //Utility class used to manage scenes.
    public static class SceneManagementUtils {
        public static bool IsCurrentScene(string name) {
            return SceneManager.GetActiveScene().name == name;
        }
    }
}

public enum Difficulty {
    Easy,
    Medium,
    Hard
}
