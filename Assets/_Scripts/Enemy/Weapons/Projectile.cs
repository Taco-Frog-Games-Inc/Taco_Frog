using UnityEngine;
using System.Collections;

/*
 * Source File Name: Projectile.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: October 12th, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: October 12th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script provides functionality for a projectile.
 * 
 * Revision History:
 *      -> October 12th, 2024:
 *          -Created this script and fully implemented it.
 */
public class Projectile : MonoBehaviour, IDamager
{
    [Header("Properties")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private int _damageToApply;

    [Header("Directional Properties")]
    private GameObject player;
    private Vector3 movement;
    public int DamageToApply { get { return _damageToApply;  } set { if (value > 0) _damageToApply = value; } }
    
    /// <summary>
    /// Initializes a projectile's direction by targetting the player.
    /// </summary>
    void Start() {
        player = GameObject.Find("Player_1");

        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        movement = speed * Time.deltaTime * direction;
        StartCoroutine(Destroy(10));
    }
    
    void Update() { SetMovement(movement); }

    /// <summary>
    /// Updates the projectile's position based on the direction.
    /// </summary>
    /// <param name="direction"></param>
    private void SetMovement(Vector3 direction) { transform.position += direction; }

    /// <summary>
    /// Parts of the IDamager contract.
    /// Checks if the other collider is implementing the IDamageTaker interface.
    /// It if does, calls the TakeDamage() of the IDamageTaker interface.
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other) {
        IDamageTaker damageTaker = other.gameObject.GetComponent<IDamageTaker>();
        damageTaker?.TakeDamage(DamageToApply);
    }

    /// <summary>
    /// Destroys the projectile after a number of seconds.
    /// </summary>
    /// <param name="waitForSeconds"></param>
    /// <returns></returns>
    private IEnumerator Destroy(float waitForSeconds) {
        yield return new WaitForSeconds(waitForSeconds);
        Destroy(gameObject);
    }
}
