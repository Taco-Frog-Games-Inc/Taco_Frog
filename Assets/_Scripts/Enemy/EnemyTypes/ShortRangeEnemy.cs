using UnityEngine;
/*
 * Source File Name: ShortRangeEnemy.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: October 2nd, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: December 5th, 2024
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
 *      -> November 24th, 2024:
 *          -Moved TakeDamage() to EnemyController
 *      -> December 5th, 2024:
 *          -Adjusted for difficulty
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
        navMeshAgent.speed *= SpawnManagerABL.EnemySpeed;
    }

    public override void Attack() { }

    public override void StopAttack() { }
}
