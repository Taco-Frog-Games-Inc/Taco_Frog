using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnhanceJumper : MonoBehaviour
{

    [SerializeField] float _jumpBoost = 35f;
    [SerializeField]  JumpSoundChannel _jumpChannel;
   // _playerPrefab;
    const string playerTag = "Player";

    IAbilityTaker _ability;

     void OnTriggerEnter(Collider other)
    {
        var playerScr = other.GetComponent<PlayerController>();
         
        
        if(other.CompareTag(playerTag))
        {   
            _jumpChannel.Invoke(true);
            _ability = new JumpAbility(other.GetComponent<PlayerController>(), _jumpBoost);
            Debug.Log("Trigger activated!");
            
           playerScr.SetHeight( _ability.GetJumpHeight());
            //this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            Destroy(this.gameObject);
        }
    }
}
