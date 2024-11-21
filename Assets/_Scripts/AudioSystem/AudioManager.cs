using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    [SerializeField] List<AudioClip> audioClipLibrary = new List<AudioClip>();

    [SerializeField] List<string> audioNames = new List<string>();

    AudioSource audioSource;
    private Dictionary<string,AudioClip> audioLibrary = new Dictionary<string, AudioClip>();
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        
        audioNames = PlayerAudioSystem.GetPlayerAudioNames();
       foreach (var clip in audioClipLibrary)
       {
            audioLibrary.Add(clip.name, clip);
       }

       Debug.Log($"AudioLib: {audioLibrary.ContainsKey(audioClipLibrary[0].name)}");
    }

    // Update is called once per frame
    public void PlayWalking()
    {
         // Check if the clip exists in the dictionary by name
        if (audioLibrary.ContainsKey(audioNames[0]))
        {
            AudioClip walkingClip = audioLibrary[audioNames[0]];
        
            // Use PlayOneShot to play the clip once
            audioSource.clip = walkingClip;
            audioSource.loop = true;

            audioSource.Play();
         }
        else
        {
        Debug.LogWarning("Walking sound not found in audio library.");
        }
    }

    public void PlayJumping()
    {
        PlayOneShotclip(1);
    }

    public void PlayLanding()
    {
        PlayOneShotclip(2);
    }

    public void PlayGettingDamage()
    {
        PlayOneShotclip(3);
    }

    private void PlayOneShotclip(int value)
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


   
}
