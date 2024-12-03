using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Source File Name: LevelEnd.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: October 3rd, 2024
 * 
 * Last Modified by: Alexander Maynard
 * Last Modified Date: November 30th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script handles the level end condition after the Taco is collected.
 * 
 * Revision History:
 *      -> October 3rd, 2024:
 *          -Created this script and fully implemented it.
 *      -> October 5th, 2024:
 *          -Added check for the player so that level end isn't called by everything.
 *      -> November 10th, 2024:
 *          -Adjusted for multiplayer. 
 *      -> November 30th, 2024:
 *          -Added scene transition fade
 */

public class LevelEnd : MonoBehaviour
{
    //when something hits the collider...
    private void OnTriggerStay(Collider other)
    {
        //...check if it's the player
        //if (other.gameObject.CompareTag("Player1") || (PlayerPrefs.GetInt("NumbOfPlayer") == 2 && other.gameObject.CompareTag("Player2")))
        //{
        //    //.. increment saving logic if it's selected? --> iteration 2. For now just need level end bringing the player to the win screen.
        //    SceneManager.LoadScene("WinScreen"); //load the win screen
        //}

        //...check if it's the player
        if (other.gameObject.CompareTag("Player"))
        {
            //update the score before the next scene change
            SaveManager.Instance.UpdateCurrentScore();
            //.. increment saving logic if it's selected? --> iteration 2. For now just need level end bringing the player to the win screen.
            SceneChangeFade.Instance.AddSceneFade(); //scene transition fade
            SceneManager.LoadScene("WinScreen"); //load the win screen
        }
    }
}
