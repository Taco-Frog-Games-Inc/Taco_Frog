using UnityEngine;

/*
 * Source File Name: PauseController.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: October 3rd, 2024
 * 
 * Last Modified by: Alexander Maynard
 * Last Modified Date: December 4th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script turns the in-game pause menu on/off each time one of the players presses 'ESC'.
 * 
 * Revision History:
 *      -> October 3rd, 2024:
 *          -Created this script and fully implemented it.
 *      -> December 4th, 2024:
 *          -Removed unecessary methods.
 */

public class PauseController : MonoBehaviour
{
    //refrence to the pause menu ui
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private PlayerController _playerController;
    private GameObject player1;
    private GameObject player2;

    //check to ensure we know when the pause menu is paused or not
    private bool _isPaused = false;

    /// <summary>
    /// Make sure to always set the pause menu off at OnStart.
    /// </summary>
    void Start()
    {
        //make sure the pause menu is off at the start
        _pauseMenu.SetActive(false);
        _playerController = transform.GetComponent<PlayerController>();
        player1 = GameObject.Find("Player1");
        if(PlayerPrefs.GetInt("NumbOfPlayer") == 2) player2 = GameObject.Find("Player2");
    }

    /// <summary>
    /// This method is called once the player presses 'ESC' (defined in the new input system controls for 'Menu'
    /// </summary>
    private void OnPause()
    {
        //chekc if game is already paused
        switch (_isPaused)
        {
            //if not paused then pause the game and activate the menu
            case false:
                Time.timeScale = 0.0f;
                _pauseMenu.SetActive(true);
                _isPaused = true;
                _playerController.playerData.hasPressedPause = true;
                player1.transform.GetChild(0).GetComponent<PlayerController>().activeScreen.SetActive(false);
                if (PlayerPrefs.GetInt("NumbOfPlayer") == 2) player2.transform.GetChild(0).GetComponent<PlayerController>().activeScreen.SetActive(false);

                break;
            //if pasued then resume the game it and de-activate the menu
            case true:
                Time.timeScale = 1.0f;
                _pauseMenu.SetActive(false);
                _isPaused = false;
                _playerController.playerData.hasPressedPause = false;
                player1.transform.GetChild(0).GetComponent<PlayerController>().activeScreen.SetActive(true);
                if (PlayerPrefs.GetInt("NumbOfPlayer") == 2) player2.transform.GetChild(0).GetComponent<PlayerController>().activeScreen.SetActive(true);
                break;
        }
    }
}
