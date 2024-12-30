using UnityEngine;

public class TutorialPlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private GameObject _player1;
    private GameObject _player2;
    
    void Awake() {
        PlayerPrefs.SetInt("NumbOfPlayer", 2);
        Vector3 pos = new(transform.position.x, 2f, transform.position.z);
        _player1 = Instantiate(player, pos, Quaternion.identity);
        _player1.name = "Player1";
        _player1.transform.GetChild(0).gameObject.GetComponent<PlayerController>().InitPlayer();

        if (PlayerPrefs.GetInt("NumbOfPlayer") == 2) {
            Vector3 pos2 = new(transform.position.x - 2f, 2f, transform.position.z);
            _player2 = Instantiate(player, pos2, Quaternion.identity);
            _player2.name = "Player2";
            _player2.transform.GetChild(0).gameObject.GetComponent<PlayerController>().InitPlayer();
        }
    }
}
