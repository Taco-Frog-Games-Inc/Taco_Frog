using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Source File Name: MasterSoundVolumeSetter.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: October 3rd, 2024
 * 
 * Last Modified by: Alexander Maynard
 * Last Modified Date: October 3rd, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script handles setting the master volume in the game when the player first loads in.
 * 
 * Revision History:
 *      -> October 3rd, 2024:
 *          -Created this script and fully implemented it.
 *              
 */

public class MasterVolumeSetter : MonoBehaviour
{
    /// <summary>
    /// Gets the previously set volume level if it exists, if not set it to max.
    /// </summary>
    void Start()
    {
        if (!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume", 1);
            AudioListener.volume = PlayerPrefs.GetFloat("masterVolume");
        }
        else
        {
            AudioListener.volume = PlayerPrefs.GetFloat("masterVolume");
        }
    }
}
