using TMPro;
using UnityEngine;

public class ScoreLoader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tacoRecordTxt;
    [SerializeField] private TextMeshProUGUI _scoreRecordTxt;
    public bool isSinglePlayer;

    // Start is called before the first frame update
    void Start()
    {
        if(isSinglePlayer)
        {
            _tacoRecordTxt.text = $"Most Tacos Recorded: {SaveManager.Instance.LoadSinglePlayerSaveData().Item1.ToString()}";
            _scoreRecordTxt.text = $"Highest Score Recorded: {SaveManager.Instance.LoadSinglePlayerSaveData().Item2.ToString()}";
        }
        else
        {
            _tacoRecordTxt.text = $"Most Tacos Recorded: {SaveManager.Instance.LoadMultiplayerSaveData().Item1.ToString()}";
            _scoreRecordTxt.text = $"Highest Score Recorded: {SaveManager.Instance.LoadMultiplayerSaveData().Item2.ToString()}";
        }
    }
}
