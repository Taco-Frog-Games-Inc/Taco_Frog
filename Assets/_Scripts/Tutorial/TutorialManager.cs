using UnityEngine;
using TMPro;

/*
 * Source File Name: TutorialManager.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: November 24th, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: November 24th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script provides functionality for tutorial.
 * 
 * Revision History:
 *      -> November 24th, 2024:
 *          -Created this script and fully implemented it.
 */

public class TutorialManager : MonoBehaviour {

    [Header("Tutorial Objects")]
    [SerializeField] private GameObject player;
    [SerializeField] private TMP_Text instruction;

    public static (int EnemyKilled, int CollectableItems) conditions = (0, 0);
    public static string instructionText = string.Empty;
       
    /// <summary>
    /// Initializes the players based on the mode selected.
    /// Multiplayer creates two instances of the player. 
    /// Singleplayer creates one.
    /// </summary>
    void Awake() {        
        Vector3 pos = new(transform.position.x, 2f, transform.position.z);
        GameObject player1 = Instantiate(player, pos, Quaternion.identity);
        player1.name = "Player1";

        if (PlayerPrefs.GetInt("NumbOfPlayer") == 2) {
            Vector3 pos2 = new(transform.position.x - 2f, 2f, transform.position.z);
            GameObject player2 = Instantiate(player, pos2, Quaternion.identity);
            player2.name = "Player2";
        }        
    }

    /// <summary>
    /// Initializes the instructions.
    /// </summary>
    private void Start() {
        if (PlayerPrefs.GetInt("NumbOfPlayer") == 2)
            instructionText = string.Format("{0,-30}{1,30}", "Player 1:", "Player 2:") + "\n" +
                              string.Format("{0,-30}{1,30}", "Use WASD to move", "Left stick to move") + "\n" +
                              string.Format("{0,-30}{1,30}", "Shift to jump", "X to jump") + "\n" +
                              string.Format("{0,-30}{1,30}", "E for tongue attack", "Square for tongue attack") + "\n" +
                              string.Format("{0,-30}{1,30}", "Esc for Pause", "Triangle for pause");
        else instructionText = "Use WASD to move\n" + "Shift to jump\n" + "E for tongue attack";
    }

    /// <summary>
    /// Checks if the instruction text has been updated. 
    /// </summary>
    void Update() {
        if (instructionText == string.Empty) return;

        SetInstructionText();
    }

    /// <summary>
    /// Displayes the instruction menu.
    /// </summary>
    private void SetInstructionText() {
        transform.GetChild(0).gameObject.SetActive(true);
        instruction.text = instructionText;
        Time.timeScale = 0.0f;
    }

    /// <summary>
    /// Hides the instruction menu.
    /// </summary>
    public void OnOkay_Pressed() {
        transform.GetChild(0).gameObject.SetActive(false);
        instructionText = string.Empty;
        Time.timeScale = 1.0f;
    }
}
