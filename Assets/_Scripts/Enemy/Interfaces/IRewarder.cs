using UnityEngine;

/*
 * Source File Name: IRewarder.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: October 14th, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: October 14th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script provides a contract for game objects that have the ability to give points to another game objet
 * 
 * Revision History:
 *      -> October 14th, 2024:
 *          -Created this script and fully implemented it.
 */
public interface IRewarder
{
    public int RewardToGive { get; set; }        
    public void OnTriggerEnter(Collider other);
}
