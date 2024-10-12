using UnityEngine;
/*
 * Source File Name: IDamager.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: October 2nd, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: October 2nd, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script provides a contract for game objects that have the ability to damage other game objects
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 */
public interface IDamager
{
    public int DamageToApply { get; set; }
    public void OnTriggerEnter(Collider other);
}