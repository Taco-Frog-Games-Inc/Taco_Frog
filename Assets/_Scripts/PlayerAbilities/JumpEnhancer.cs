using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class JumpEnhancer : MonoBehaviour
{

    [SerializeField] float _jumpBoost = 35f;
   // _playerPrefab;
    const string playerTag = "Player";
    [SerializeField] AudioClip _jumpAbilityPick;
    private AudioSource _audioSource;
    IAbilityTaker _ability;

   void Start()
   {
     _audioSource = GetComponent<AudioSource>();
   }
    void OnTriggerEnter(Collider other)
    {
        var playerScr = other.GetComponent<PlayerController>();
        if(other.CompareTag(playerTag))
        {   
            _audioSource.PlayOneShot(_jumpAbilityPick);
            _ability = new JumpAbility(other.GetComponent<PlayerController>(), _jumpBoost);
            Debug.Log("Trigger activated!");
            
           playerScr.SetHeight( _ability.GetJumpHeight());
            //this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            Destroy(this.gameObject);
        }
    }
}
