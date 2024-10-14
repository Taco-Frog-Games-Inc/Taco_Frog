using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Source File Name: MenuCanvasController.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: October 14th, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: October 14th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script provides functionnality for the HUD based on single or multi-player mode. 
 * 
 * Revision History:
 *      -> October 14th, 2024:
 *          -Created this script and fully implemented it.
 */
public class MenuCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject splitScreenHUDLeft;
    [SerializeField] private GameObject splitScreenHUDRight;
    private GameObject playerInputManager;

    private int playerCount = 0;
    private int initialPlayercount = 0;

    private void Start() { playerInputManager = GameObject.Find("PlayerManager"); }
    
    /// <summary>
    /// Checks if the number of player counts has changed. 
    /// If there is one player, show the main HUD
    /// If there are two players, show two HUDs, one for each.
    /// </summary>
    void Update()
    {
        if(playerInputManager != null) 
            playerCount = playerInputManager.gameObject.GetComponent<PlayerInputManager>().playerCount;
        
        //No point to continue
        if (playerCount == 0) return;

        //Only trigger if the inital value is different than the current value
        if (initialPlayercount != playerCount) {
            if (playerCount == 1)
            {
                HUD.SetActive(true);
                splitScreenHUDLeft.SetActive(false);
                splitScreenHUDRight.SetActive(false);
            }
            else if (playerCount == 2)
            {
                HUD.SetActive(false);
                splitScreenHUDLeft.SetActive(true);
                splitScreenHUDRight.SetActive(true);
            }

            initialPlayercount = playerCount;
        }        
    }
}
