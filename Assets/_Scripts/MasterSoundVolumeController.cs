using UnityEngine;
using UnityEngine.UI;

/*
 * Source File Name: MasterSoundVolumeController.cs
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
 *      This script handles the master volume in the game.
 * 
 * Revision History:
 *      -> October 3rd, 2024:
 *          -Created this script and fully implemented it.
 *              
 */

public class MasterSoundVolumeController : MonoBehaviour
{
    //reference to the options menu master volume slider
    [SerializeField] Slider volumeSlider;


    /// <summary>
    /// Gets the volume settings or sets it for the first time
    /// </summary>
    void Start()
    {
        if(!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume", 1);
        }
        else
        {
            LoadMasterVolume(); //load the previously sets volume level
        }
    }

    /// <summary>
    /// Changes the AudioListener volume across the game.
    /// </summary>
    public void ChangeMasterVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveMasterVolume();
    }


    /// <summary>
    /// Set the slider to the master volume level
    /// </summary>
    private void LoadMasterVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
    }

    /// <summary>
    /// Save the master volume level based on the slider.
    /// </summary>
    private void SaveMasterVolume()
    {
        PlayerPrefs.SetFloat("masterVolume", volumeSlider.value);
    }
}
