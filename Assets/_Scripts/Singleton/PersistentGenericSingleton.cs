using UnityEngine;

/*
 * Source File Name: PersistentSingleton.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: November 9th, 2024
 * 
 * Last Modified by: Alexander Maynard
 * Last Modified Date: November 9th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script enforces the persistent singleton pattern for other manager objects
 * 
 * Revision History:
 *      ->  November 9th, 2024:
 *          -Created and completed basic functionality of this script.
 */

/// <summary>
/// This class enforces the singleton pattern.
/// </summary>
/// <typeparam name="T">Generic type for any type of object wanting to enforce the singleton.</typeparam>
public abstract class PersistGenericSingleton<T> : MonoBehaviour where T : Component
{
    //public instance with private set
    [HideInInspector] public static T Instance { get; private set; }

    //on awake validate so that only one instance is in the game at all times.
    protected virtual void Awake()
    {
        //if there is no instance or the instance is not of this type...
        if (Instance != null && Instance != this)
        {
            //destroy the gameobject
            Destroy(this.gameObject);
        }
        //if there is no instance of this object...
        else
        {
            //create a new instance
            Instance = this as T;
            //make sure it is not destroyed
            DontDestroyOnLoad(Instance);
        }
    }
}