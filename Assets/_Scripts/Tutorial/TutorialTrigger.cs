using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Source File Name: TutorialTrigger.cs
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
public class TutorialTrigger : MonoBehaviour {

    [SerializeField] private string instruction;

    private readonly string levelEndInstructionText = "You must complete the tutorial before proceeding.";
    private string prevObj = "";
    private string currObj = "";
    
    /// <summary>
    /// Checks what object collides with the item.
    /// Acts upon the collision.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        
        if (other.name.Contains("Player")) {
            currObj = gameObject.name;
            TutorialManager.instructionText = instruction;
            if (!gameObject.name.Equals("LevelEndTacoTutorial") && prevObj != currObj) {
                TutorialManager.conditions.CollectableItems++;
                prevObj = currObj; //Ensures object does not collide twice.
            } 
            else {
                if (TutorialManager.conditions.CollectableItems != 4 || TutorialManager.conditions.EnemyKilled != 2) 
                    TutorialManager.instructionText = levelEndInstructionText;
                                                    
                else {
                    TutorialManager.instructionText = string.Empty;
                    PlayerPrefs.SetInt("HasDoneTutorial", 1); //If it reaches that point, tutorial completed
                    SceneManager.LoadScene("WinScreen");
                }
            }                                          
        }                                               
    }             
}
