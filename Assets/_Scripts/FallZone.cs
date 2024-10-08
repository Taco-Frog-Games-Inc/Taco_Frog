using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Source File Name: FallZone.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: October 5th, 2024
 * 
 * Last Modified by: Alexander Maynard
 * Last Modified Date: October 5th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script handles the fallzone for teh player.
 * 
 * Revision History:
 *      -> October 3rd, 2024:
 *          -Created this script and fully implemented it for now (until iteration 2)
 */

public class FallZone : MonoBehaviour
{
   //when something hits the collider...
    private void OnTriggerEnter(Collider other)
    {
        //...check if it's the player
        if (other.gameObject.tag == "Player")
        {
            //.. increment saving logic if it's selected? --> iteration 2. For now just need level end bringing the player to the win screen.

            SceneManager.LoadScene("LoseScreen"); //load the lose screen for now... Iteration 2 this will need to be updated.
        }
    }
}
