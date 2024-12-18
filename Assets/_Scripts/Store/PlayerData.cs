using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Events/ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject {

    public new string name;

    public float jumpHeight;
    public float tongueAttackLength;
    public float speed;
    
    public bool hasPressedPause;

    public int health;
    public int moneyValue;

    public RenderTexture minimapTexture;

}
