/*
 * Source File Name: IAttack.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: October 2nd, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: October 12th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script provides a contract for game objects that have the ability to attack
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 *      -> October 12th, 2024:
 *          -Added StopAttack()
 */
public interface IAttack
{
    void Attack();
    void StopAttack();
}
