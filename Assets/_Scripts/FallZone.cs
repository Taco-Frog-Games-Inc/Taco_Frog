using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Source File Name: FallZone.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: October 5th, 2024
 * 
 * Last Modified by: Alexander Maynard
 * Last Modified Date: November 30th, 2024
 * 
 * Program Description: 
 *      
 *      This script handles the fallzone for teh player.
 * 
 * Revision History:
 *      -> October 3rd, 2024:
 *          -Created this script and fully implemented it for now (until iteration 2)
 *      -> November 30th, 2024:
 *          -Added scene transition fade
 */

public class FallZone : MonoBehaviour
{
   //when something hits the collider...
    private void OnTriggerEnter(Collider other)
    {
        //...check if it's the player
        if (other.gameObject.tag == "Player")
        {
            //make sure score is updated properly before death (scene transition)
            SaveManager.Instance.UpdateCurrentScore();
            SceneChangeFade.Instance.AddSceneFade(); //scene transition fade
            SceneManager.LoadScene("LoseScreen"); //load the lose screen for now... Iteration 2 this will need to be updated.
        }
    }
}
