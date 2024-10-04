using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Source File Name: PlayerController.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: October 3rd, 2024
 * 
 * Last Modified by: Alexander Maynard
 * Last Modified Date: October 3rd, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script handles the level end condition after the Taco is collected.
 * 
 * Revision History:
 *      -> October 3rd, 2024:
 *          -Created this script and fully implemented it.
 */

public class LevelEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay()
    {
        //.. increment saving logic if it's selected? --> iteration 2. For now just need level end bringing the player to the win screen.

        SceneManager.LoadScene("WinScreen");
    }
}
