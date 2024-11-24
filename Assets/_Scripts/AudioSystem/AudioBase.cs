using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class  AudioBase : MonoBehaviour
{
    protected AudioSource audioSource;
    protected  List<string> enemyAudioNames = new List<string>();
    protected List<string> audioNames = new List<string>();
    
    protected readonly Dictionary<string,AudioClip> enemyAudioLibrary = new Dictionary<string, AudioClip>();
    protected readonly Dictionary<string,AudioClip> audioLibrary = new Dictionary<string, AudioClip>();

   

     protected void GetEnemyAudioNames(List<AudioClip> audioClips)
    {
        enemyAudioNames  = EnemyAudioSystem.GetEnemyAudioNames();
       
        foreach (var clip in audioClips)
           enemyAudioLibrary.Add(clip.name, clip);
       
    
        
    }

      protected void GetPlayerAudioNames(List<AudioClip> playerAudioClips)
    {
        
        audioNames = PlayerAudioSystem.GetPlayerAudioNames();
       
        foreach (var clip in playerAudioClips)
            audioLibrary.Add(clip.name, clip);
    
        
    }

  

  

}