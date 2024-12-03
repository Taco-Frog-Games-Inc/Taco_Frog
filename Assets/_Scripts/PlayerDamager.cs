using UnityEngine;

/*
 * Source File Name: PlayerDamager.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: October 14th, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: October 16th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script is connected to the players weapon - it damages the enemy.
 * 
 * Revision History:
 *      -> October 14th, 2024:
 *          -Created this script and fully implemented it. 
 *      -> October 16th, 2024:
 *          -Segregated large function into smaller ones
 *          -Added damage applied to different enemy types.
 */
public class PlayerDamager : MonoBehaviour, IDamager
{
    private int _damageToApply;
    public int DamageToApply { get { return _damageToApply; } set { if (value > 0) _damageToApply = value; } }
    private AudioSource _ads;
    [SerializeField] AudioClip _aclip, _aSecClip;
    [SerializeField] JumpSoundChannel _jmpSnd;
    /// <summary>
    /// Parts of the IDamager contract.
    /// Checks if the other collider is implementing the IDamageTaker interface.
    /// It if does, calls the TakeDamage() of the IDamageTaker interface.
    /// </summary>
    /// <param name="other"></param>
    /// 
    void Start()
    {
        _ads = GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other) {
        GameObject parent = gameObject.transform.parent.gameObject;
        if (gameObject.CompareTag("EnemySquasher") && other.gameObject.CompareTag("EnemyHead") &&
            parent.GetComponent<PlayerController>()._velocity.y < 1f)
                DoEnemyHead_Damage(other);

        else if (this.gameObject.CompareTag("Tongue") && other.gameObject.CompareTag("Enemy"))        
            DoTongue_Damage(other);        
    }

    /// <summary>
    /// Applies damage to enemy based on the enemy type
    /// </summary>
    /// <param name="other"></param>
    private void DoEnemyHead_Damage(Collider other) {
        GameObject enemy = other.transform.parent.transform.parent.gameObject;
       // _ads.PlayOneShot(_aSecClip);
        if (enemy.GetComponent<ShortRangeEnemy>()) _damageToApply = 50;
        else if (enemy.GetComponent<LongRangedEnemy>()) _damageToApply = 100;

        IDamageTaker damageTaker = enemy.GetComponent<IDamageTaker>();
        damageTaker?.TakeDamage(DamageToApply);
        

    }

    /// <summary>
    /// Applies tongue damage to enemy based on the enemy type
    /// </summary>
    /// <param name="other"></param>
    private void DoTongue_Damage(Collider other)
    {
        GameObject enemy = other.gameObject;

        if (enemy.GetComponent<ShortRangeEnemy>()) _damageToApply = 10;                   
        else if (enemy.GetComponent<LongRangedEnemy>()) _damageToApply = 20;

        IDamageTaker damageTaker = enemy.GetComponent<IDamageTaker>();
        damageTaker?.TakeDamage(DamageToApply);
        EnemyGotDamageSound();
    }
     public void EnemyGotDamageSound()
    {
         _ads.PlayOneShot(_aclip);
    }
}
