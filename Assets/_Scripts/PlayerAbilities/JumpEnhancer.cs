using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnhancer : MonoBehaviour
{

    [SerializeField] float _jumpBoost = 35f;
   // _playerPrefab;
    const string playerTag = "Player";

    IAbilityTaker _ability;

    
    void OnTriggerEnter(Collider other)
    {
        var playerScr = other.GetComponent<PlayerController>();
        if(other.CompareTag(playerTag))
        {
            _ability = new JumpAbility(other.GetComponent<PlayerController>(), _jumpBoost);
            Debug.Log("Trigger activated!");
           playerScr.SetHeight( _ability.GetJumpHeight());
            //this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            Destroy(this.gameObject);
        }
    }
}
