using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

/*
 * Source File Name: EnemyController.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: October 2nd, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: November 11th, 2024
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
 *      -> October 14th, 2024:
 *          -Adjusted the nextWayPointIndex's and health's visibility
 *          -Removed testing statements
 *      -> October 16th, 2024:
 *          -Implemented IDamager
 *      -> November 10th, 2024:
 *          -Implemented the start method and changed the signature of the SensePlayer and 
 *          EngagePlayer methods.
 *      -> November 11th, 2024:
 *          -Adapted for testing scene.
 */
public abstract class EnemyController : MonoBehaviour, IAttack, IDamageTaker, IDamager
{

    public GameObject player1;
    public GameObject player2;
    public EnemyStateMachine stateMachine;
    protected internal NavMeshAgent navMeshAgent;

    [Header("Internal Properties")]
    public float EnemyFOV = 89f;
    protected internal float cosEnemyFOVover2InRAD;
    public float closeEnoughEngageCutoff = 30f;
    public float closeEnoughSenseCutoff = 45f;
    protected internal bool isDead = false;

    [Header("Path")]
    public GameObject path;
    protected internal int nextWayPointIndex;

    [Header("InGame Properties")]
    [SerializeField] protected internal int health = 100;
    public int Health { get { return health;  } set { if (value > 0) health = value; } }
    [SerializeField] private int damageToApply;

    private int _defaultDamageToApply = 10;
    private bool isTestingScene;
    public int DamageToApply { get { return _defaultDamageToApply; } set { if (value > 0) _defaultDamageToApply = value; } }
    private void Awake() { stateMachine = new(); }
    public void Start() {
        isTestingScene = SceneManager.GetActiveScene().name == "TestingStaticScene";
        player1 = GameObject.Find("Player1").transform.GetChild(0).gameObject;
        if(!isTestingScene && PlayerPrefs.GetInt("NumbOfPlayer") == 2)
            player2 = GameObject.Find("Player2").transform.GetChild(0).gameObject;

        cosEnemyFOVover2InRAD = Mathf.Cos(EnemyFOV / 2f * Mathf.Deg2Rad); 
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public void FixedUpdate() { stateMachine.FixedUpdate(); }
    
    /// <summary>
    /// Checks if the enemy senses the player based on its position and field of view.
    /// </summary>
    /// <returns></returns>
    protected internal (GameObject, bool) SensePlayer() {
        if (EnemyUtilities.SenseOther(gameObject, player1, cosEnemyFOVover2InRAD, closeEnoughSenseCutoff)) 
            return (player1, true);                    
                    
        if (!isTestingScene && PlayerPrefs.GetInt("NumbOfPlayer") == 2 && 
            EnemyUtilities.SenseOther(gameObject, player2, cosEnemyFOVover2InRAD, closeEnoughSenseCutoff))
            return (player2, true);

        return (null, false);
    }    

    /// <summary>
    /// Checks if the condition to engage a player has been met.
    /// </summary>
    /// <returns></returns>
    protected internal (GameObject, bool) EngagePlayer() {
        if(EnemyUtilities.SenseOther(gameObject, player1, cosEnemyFOVover2InRAD, closeEnoughEngageCutoff))
            return (player1, true);
        if(!isTestingScene && PlayerPrefs.GetInt("NumbOfPlayer") == 2 && 
           EnemyUtilities.SenseOther(gameObject, player2, cosEnemyFOVover2InRAD, closeEnoughEngageCutoff))
            return (player2, true);

        return(null, false);               
    }

    /// <summary>
    /// Parts of the IDamager contract.
    /// Checks if the other collider is implementing the IDamageTaker interface.
    /// It if does, calls the TakeDamage() of the IDamageTaker interface.
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        /*if (gameObject.CompareTag("Enemy") && other.gameObject.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            IDamageTaker damageTaker = player.GetComponent<IDamageTaker>();
            damageTaker?.TakeDamage(DamageToApply);
        }*/
    }
    public abstract void Attack();
    public abstract void StopAttack();
    public abstract void TakeDamage(int damage);

}
