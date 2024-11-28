using TMPro;
using UnityEngine;

/*
 * Source File Name: FPSCounterManager.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: November 28th, 2024
 * 
 * Last Modified by: Alexander Maynard
 * Last Modified Date: November 28th, 2024
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
 */

public class FPSCounterManager : PersistGenericSingleton<FPSCounterManager>
{
    [SerializeField] private TextMeshProUGUI _franeRateText; //text to display the frameRate
    [SerializeField] private int _targetFrameRate = 30;
    private float _timeBetweenFrameCount = 1f; //make sure that we are checking every second
    private float _time; // keep track of current time
    private int _frameCount; //keep track of the frame count

    private void Start()
    {
        Application.targetFrameRate = _targetFrameRate; //set the target fps --> 30 was chosen for mow so that we don't have many frame dips compared to a target of 60fps
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
            int frameRate = Mathf.RoundToInt(_frameCount / _time); //get the framerate by dividing the frameCount (how many frames occured in the update) by the actual time that has occured

            _franeRateText.text = $"FPS: {frameRate.ToString()}"; //display the frameRate

            _time -= _timeBetweenFrameCount; //not resetting due to timing might being sligthly off.
            _frameCount = 0; //reset the frameCount
        }
    }
}
