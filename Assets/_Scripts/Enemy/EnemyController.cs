using UnityEngine;
using UnityEngine.AI;

/*
 * Source File Name: EnemyController.cs
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
 *      This script provides functionality for an enemy controller.
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 *      -> October 12th, 2024:
 *          -Removed the implementation of the IDamage
 */
public abstract class EnemyController : MonoBehaviour, IAttack, IDamageTaker
{
    public GameObject player;
    public EnemyStateMachine stateMachine;
    protected internal NavMeshAgent navMeshAgent;

    //Testing purposes
    protected internal Color initialColor;

    [Header("Internal Properties")]
    public float EnemyFOV = 89f;
    protected internal float cosEnemyFOVover2InRAD;
    public float closeEnoughEngageCutoff = 30f;
    public float closeEnoughSenseCutoff = 45f;
    protected internal bool isDead = false;

    [Header("Path")]
    public GameObject path;
    public int nextWayPointIndex = 0;

    [Header("InGame Properties")]
    [SerializeField] private int health = 100;
    public int Health { get { return health;  } set { if (value > 0) health = value; } }
    [SerializeField] private int damageToApply;

    private void Awake() { stateMachine = new(); }

    public void Start() { 
        cosEnemyFOVover2InRAD = Mathf.Cos(EnemyFOV / 2f * Mathf.Deg2Rad); 
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void FixedUpdate() { stateMachine.FixedUpdate(); }
    
    /// <summary>
    /// Checks if the enemy senses the player based on its position and field of view.
    /// </summary>
    /// <returns></returns>
    protected internal bool SensePlayer() {        
        return EnemyUtilities.SenseOther(gameObject, player, cosEnemyFOVover2InRAD, closeEnoughSenseCutoff);
    }    

    /// <summary>
    /// Checks if the condition to engage a player has been met.
    /// </summary>
    /// <returns></returns>
    protected internal bool EngagePlayer() {
        return EnemyUtilities.SenseOther(gameObject, player, cosEnemyFOVover2InRAD, closeEnoughEngageCutoff);
    }
    public abstract void Attack();
    public abstract void StopAttack();
    public abstract void TakeDamage(int damage);
}
