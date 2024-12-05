using TMPro;
using UnityEngine;

/*
 * Source File Name: FPSCounterManager.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: November 28th, 2024
 * 
 * Last Modified by: Alexander Maynard
 * Last Modified Date: December 4th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script handles the frame rate and displaying of the framerate in the game scene.
 * 
 * 
 * Revision History:
 *      -> November 28th, 2024:
 *          -Created this script and fully implemented it.
 *      -> November 29th 2024:
 *          -Improved the FPS counter
 *      -> December 4th, 2024:
 *          -Added a variable fps based on quality settings.
 */

public class FPSCounterManager : PersistGenericSingleton<FPSCounterManager>
{
    [SerializeField] private TextMeshProUGUI _franeRateText; //text to display the frameRate
    private float _timeBetweenFrameCount = 1f; //make sure that we are checking every second
    private float _time; // keep track of current time
    private int _frameCount; //keep track of the frame count

    private void Start()
    {
        //set fps to 60 if qaulity settings are from very low to medium 
        if (QualitySettings.GetQualityLevel() == 0 || QualitySettings.GetQualityLevel() == 1 || QualitySettings.GetQualityLevel() == 2)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
        //set fps to 30 if quality settings are from very high to ultra 
        else if (QualitySettings.GetQualityLevel() == 3 || QualitySettings.GetQualityLevel() == 5 || QualitySettings.GetQualityLevel() == 5)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 30;
        }
    }
    /// <summary>
    /// Using the update loop to track the game's frames oer second by dividing the 
    /// amount of frames that occured by the acutal time that occured (will be frame per second).
    /// </summary>
    void Update()
    {
        _time += Time.deltaTime; //Increment time

        _frameCount++; //increase frame count

        if(_time >= _timeBetweenFrameCount)
        {
            float frameRate = Mathf.Round((_frameCount / _time) * 10f) / 10f; //get the framerate by dividing the frameCount (how many frames occured in the update) by the actual time that has occured

            _franeRateText.text = $"FPS: {frameRate.ToString()}"; //display the frameRate

            _time -= _timeBetweenFrameCount; //not resetting due to timing might being sligthly off.
            _frameCount = 0; //reset the frameCount
        }
    }
}
