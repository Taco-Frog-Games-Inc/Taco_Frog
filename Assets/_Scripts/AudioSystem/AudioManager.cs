using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : AudioBase
{
    [Header("AudioClip References")]
    [SerializeField] List<AudioClip> audioClipLibrary = new List<AudioClip>();

   void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        GetPlayerAudioNames(audioClipLibrary );
       
    }
    

#region 
/// <summary>
/// Calls player audios, funcitons related to player audios start
/// </summary>
    // Update is called once per frame
    public void PlayWalking() => PlayOneShotclipForPlayer(0);
    public void PlayJumping() => PlayOneShotclipForPlayer(1);
    public void PlayAttack() => PlayOneShotclipForPlayer(2);
    public void PlayPowerUpInv() => PlayOneShotclipForPlayer(3);
    public void PlayGettingDamage() => PlayOneShotclipForPlayer(4);
   
    public  void PlayOneShotclipForPlayer(int value)
    {
        if (audioLibrary.ContainsKey(audioNames[value]))
        {
            AudioClip playClip = audioLibrary[audioNames[value]];
            audioSource.PlayOneShot(playClip);
        }
        else
        {
            Debug.LogWarning("Walking sound not found in audio library.");
        }
    }
/// <summary>
/// funcitons for player audio ends
/// </summary>
#endregion



    
    



   
}
