using UnityEngine;

/*
 * Source File Name: LongRangedEnemy.cs
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
 *      This script handles the long-ranged controller by extending the abstract EnemyController class.
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 *      -> October 12th, 2024:
 *          -Added the shooting functionality.
 *          -Added the functionality of taking damage.
 *      -> October 14th, 2024:
 *          - Adjusted the TakeDamage function to destroy the object if killed
 *          - Initialize nextWayPointIndex to a random waypoint
 *      ->October 15th, 2024: 
 *          -Added animator transitions to this enemy for multiple animations to be played based on the state of the enemy.
 *      -> November 24th, 2024:
 *          -Moved TakeDamage() to EnemyController
 *      -> December 5th, 2024:
 *          -Adjusted for difficulty
 */

public class LongRangedEnemy : EnemyController
{
    [Header("Shooting-Related")]
    [SerializeField] private GameObject taco;
    [SerializeField] private Animator _animator;

    new void Start() {
        base.Start();
        stateMachine.AddState(new RoamingState(this, stateMachine));
        stateMachine.AddState(new ChasingState(this, stateMachine));
        stateMachine.AddState(new AttackingState(this, stateMachine));
        stateMachine.AddState(new DyingState(this, stateMachine));

        nextWayPointIndex = Random.Range(0, path.transform.childCount);
        navMeshAgent.speed *= SpawnManagerABL.EnemySpeed;
    }

    public override void Attack() { Shoot(); }
    private void Shoot() 
    {
        _animator.SetBool("isAttacking", true); //calls the attacking animation
        InvokeRepeating(nameof(DoShooting), 0f, 0.5f); 
    }
    public override void StopAttack() 
    {
        _animator.SetBool("isAttacking", false);
        CancelInvoke(nameof(DoShooting)); //calls the walk animation
    }
    
    /// <summary>
    /// Instantiate a bullet at the spawner level of the long-ranged enemy.
    /// </summary>
    private void DoShooting() {
        Instantiate(taco, gameObject.transform.GetChild(0).transform.GetChild(0).transform.position, Quaternion.identity);
    }
}
