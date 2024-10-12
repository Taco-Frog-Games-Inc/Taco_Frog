using UnityEngine;

/*
 * Source File Name: LongRangedEnemy.cs
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
 *      This script handles the long-ranged controller by extending the abstract EnemyController class.
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 *      -> October 12th, 2024:
 *          -Added the shooting functionality.
 *          -Added the functionality of taking damage.
 */

public class LongRangedEnemy : EnemyController
{
    [Header("Shooting-Related")]
    [SerializeField] private GameObject taco;

    new void Start() {
        base.Start();
        stateMachine.AddState(new RoamingState(this, stateMachine));
        stateMachine.AddState(new ChasingState(this, stateMachine));
        stateMachine.AddState(new AttackingState(this, stateMachine));
        stateMachine.AddState(new DyingState(this, stateMachine));
    }

    public override void Attack() { Shoot(); }
    private void Shoot() { InvokeRepeating(nameof(DoShooting), 0f, 0.5f); }
    public override void StopAttack() { CancelInvoke(nameof(DoShooting)); }
    
    /// <summary>
    /// Instantiate a bullet at the spawner level of the long-ranged enemy.
    /// </summary>
    private void DoShooting() {
        Instantiate(taco, gameObject.transform.GetChild(0).transform.GetChild(0).transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Adjusts the enemy health by substracting the damage taken.
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamage(int damage) {
        Health -= damage;
        if(Health < 0) Health = 0;
    }
}
