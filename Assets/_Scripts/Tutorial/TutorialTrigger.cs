using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private string instruction;

    private readonly string levelEndInstructionText = "You must complete the tutorial before proceeding.";

    private void OnTriggerEnter(Collider other) {               
        if (other.name.Contains("Player")) {           
            TutorialManager.instructionText = instruction;
            if (!gameObject.name.Equals("LevelEndTacoTutorial")) TutorialManager.collectableCount++;
            else
                if (TutorialManager.collectableCount != 4 || TutorialManager.enemyKilled != 2)
                TutorialManager.instructionText = levelEndInstructionText;
            else
            {
                TutorialManager.instructionText = string.Empty;
                PlayerPrefs.SetInt("HasDoneTutorial", 1);
                SceneManager.LoadScene("WinScreen");
            }                       
        }                                               
    }             
}
