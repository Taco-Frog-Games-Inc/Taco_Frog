using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/*
 * Source File Name: MenuCanvasController.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: October 14th, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: November 13th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script provides functionnality for the HUD based on single or multi-player mode. 
 * 
 * Revision History:
 *      -> October 14th, 2024:
 *          -Created this script and fully implemented it.
 *      -> November 10th, 2024:
 *          -Adjusted for multiplayer.
 *      -> November 11th, 2024:
 *          -Adjusted for testing scene.
 *      -> November 13th, 2024:
 *          -Adapted to set TacoScore
 */
public class MenuCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject splitScreenHUDLeft;
    [SerializeField] private GameObject splitScreenHUDRight;
    private GameObject playerInputManager;

    private int playerCount = 0;
    private int initialPlayercount = 0;
    public static bool isTestingScene;
    private void Start() { 
        playerInputManager = GameObject.Find("PlayerManager");
        isTestingScene = SceneManager.GetActiveScene().name == "TestingStaticScene";
    }
    
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
        if (!isTestingScene && playerCount == 0) return;

        GameObject player = transform.parent.gameObject.transform.GetChild(0).gameObject;
        //Only trigger if the inital value is different than the current value
        if (!isTestingScene && initialPlayercount != playerCount)
        {
            if (playerCount == 1) SetPlayer1UI(player);

            else if (playerCount == 2)
            {
                HUD.SetActive(false);
                if (transform.parent.name == "Player1")
                {
                    splitScreenHUDLeft.SetActive(true);
                    splitScreenHUDRight.SetActive(false);
                    SetActiveScreen(player, splitScreenHUDLeft);
                }
                else if (transform.parent.name == "Player2")
                {
                    splitScreenHUDRight.SetActive(true);
                    splitScreenHUDLeft.SetActive(false);
                    SetActiveScreen(player, splitScreenHUDRight);
                }
            }

            initialPlayercount = playerCount;
        }
        else  if (isTestingScene) SetPlayer1UI(player);        
    }

    private void SetPlayer1UI(GameObject player) {
        HUD.SetActive(true);
        splitScreenHUDLeft.SetActive(false);
        splitScreenHUDRight.SetActive(false);
        SetActiveScreen(player, HUD);       
    }

    private void SetActiveScreen(GameObject player, GameObject activeScreen) {
        player.GetComponent<PlayerController>().activeScreen = activeScreen;
        player.GetComponent<PlayerController>().activeScreen.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = SaveManager.Instance.GetTacoScore().ToString() ?? "0";
    }
}
