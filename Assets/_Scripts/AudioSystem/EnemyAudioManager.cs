using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyAudioManager : AudioBase
{
    [Header("AudioClip and name References")]
    [SerializeField] List<AudioClip> enemyAudioClipLibrary = new List<AudioClip>();
   
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
       void Start()
    {
       
        GetEnemyAudioNames(enemyAudioClipLibrary);
    }
  
    
    public void PlayEnemyGetDamage() => PlayOneShotclipForEnemy(0);
    public void Walking() => PlayOneShotclipForEnemy(1);
    public void PlayOneShotclipForEnemy(int value)
    {
        if (enemyAudioLibrary.ContainsKey(enemyAudioNames[value]))
        {
            AudioClip playClip = enemyAudioLibrary[enemyAudioNames[value]];
            audioSource.PlayOneShot(playClip);
        }
        else
        {
            Debug.LogWarning("Walking sound not found in audio library.");
        }
    }


   
}
