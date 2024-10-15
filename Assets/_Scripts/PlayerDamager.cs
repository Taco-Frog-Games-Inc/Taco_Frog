using UnityEngine;

/*
 * Source File Name: PlayerDamager.cs
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
 *      This script is connected to the players weapon - it damages the enemy.
 * 
 * Revision History:
 *      -> October 14th, 2024:
 *          -Created this script and fully implemented it. 
 */
public class PlayerDamager : MonoBehaviour, IDamager
{
    [Header("Properties")]
    [SerializeField] private int _damageToApply;
    public int DamageToApply { get { return _damageToApply; } set { if (value > 0) _damageToApply = value; } }

    /// <summary>
    /// Parts of the IDamager contract.
    /// Checks if the other collider is implementing the IDamageTaker interface.
    /// It if does, calls the TakeDamage() of the IDamageTaker interface.
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other) {
        GameObject parent = gameObject.transform.parent.gameObject;
        if (other.gameObject.CompareTag("EnemyHead") && 
            parent.GetComponent<PlayerController>()._velocity.y < 1f) { 
            GameObject enemy = other.transform.parent.transform.parent.gameObject;
            IDamageTaker damageTaker = enemy.GetComponent<IDamageTaker>();
            damageTaker?.TakeDamage(DamageToApply);
        }
    }
}
