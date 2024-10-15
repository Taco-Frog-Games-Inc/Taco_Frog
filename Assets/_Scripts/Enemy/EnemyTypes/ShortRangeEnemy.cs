using UnityEngine;
/*
 * Source File Name: ShortRangeEnemy.cs
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
 *      This script handles the short-ranged controller by extending the abstract EnemyController class.
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 *      -> October 14th, 2024:
 *          -Added the functionality of taking damage.
 *          - Removed testing statement
 *          - Initialize nextWayPointIndex to a random waypont.
 */

public class ShortRangeEnemy : EnemyController
{   
    new void Start() {
        base.Start();
        stateMachine.AddState(new RoamingState(this, stateMachine));
        stateMachine.AddState(new ChasingState(this, stateMachine));
        stateMachine.AddState(new AttackingState(this, stateMachine));
        stateMachine.AddState(new DyingState(this, stateMachine));

        nextWayPointIndex = Random.Range(0, path.transform.childCount);
    }

    public override void Attack() { }

    public override void StopAttack() { }

    /// <summary>
    /// Reduces the enemy health by substracting the damage taken.
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamage(int damage) {
        health -= damage;
        if (Health < 0) health = 0;
        if (Health == 0) Destroy(gameObject);
    }
}
