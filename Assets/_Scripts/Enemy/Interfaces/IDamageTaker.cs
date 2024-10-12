/*
 * Source File Name: IDamageTaker.cs
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
 *      This script provides a contract for game objects that have the ability to take damage from another game objet
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 */
public interface IDamageTaker 
{
    int Health { get; set; }
    void TakeDamage(int damage);
}
