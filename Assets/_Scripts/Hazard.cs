using UnityEngine;
/*
 * Source File Name: Hazard.cs
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
 *      This script provides the ability to a hazard to damage another game object that implements the IDamageTaker
 * 
 * Revision History:
 *      -> October 14th, 2024:
 *          -Created this script and fully implemented it.
 */
public class Hazard : MonoBehaviour, IDamager
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
        if (other.gameObject.CompareTag("Player")) {
            IDamageTaker damageTaker = other.GetComponent<IDamageTaker>();
            damageTaker?.TakeDamage(DamageToApply);
        }
    }
}
