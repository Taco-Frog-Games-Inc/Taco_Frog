using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private TMP_Text instruction;

    public static int enemyKilled = 0;
    public static int collectableCount = 0;
    public static string instructionText = string.Empty;
       
    void Awake() {
        PlayerPrefs.SetInt("HasDoneTutorial", 0);//TODO: REMOVE!
        Vector3 pos = new(transform.position.x, 2f, transform.position.z);
        GameObject player1 = Instantiate(player, pos, Quaternion.identity);
        player1.name = "Player1";

        if (PlayerPrefs.GetInt("NumbOfPlayer") == 2) {
            Vector3 pos2 = new(transform.position.x - 2f, 2f, transform.position.z);
            GameObject player2 = Instantiate(player, pos2, Quaternion.identity);
            player2.name = "Player2";
        }        
    }

    private void Start() {
        if (PlayerPrefs.GetInt("NumbOfPlayer") == 2)
            instructionText = string.Format("{0,-30}{1,30}", "Player 1:", "Player 2:") + "\n" +
                              string.Format("{0,-30}{1,30}", "Use WASD to move", "Left stick to move") + "\n" +
                              string.Format("{0,-30}{1,30}", "Shift to jump", "X to jump") + "\n" +
                              string.Format("{0,-30}{1,30}", "E for tongue attack", "O for tongue attack");
        else instructionText = "Use WASD to move\n" + "Shift to jump\n" + "E for tongue attack";
    }

    void Update() {
        if (instructionText == string.Empty) return;

        SetInstructionText();
    }

    private void SetInstructionText() {
        transform.GetChild(0).gameObject.SetActive(true);
        instruction.text = instructionText;
        Time.timeScale = 0.0f;
    }

    public void OnOkay_Pressed() {
        transform.GetChild(0).gameObject.SetActive(false);
        instructionText = string.Empty;
        Time.timeScale = 1.0f;
    }
}
