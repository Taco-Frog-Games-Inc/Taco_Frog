using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleCollective : MonoBehaviour
{
    [SerializeField] int _increasHealthTo = 5;
    [SerializeField] InvincibilityChannel _invChannel; 
    const string PlayerTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PlayerTag)) {return;}
        _invChannel.Invoke(true);
        Destroy(gameObject);
    }
}
