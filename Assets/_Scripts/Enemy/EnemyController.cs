using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyController : MonoBehaviour, IAttack, IDamager, IDamageTaker
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

    public int DamageToApply { get { return damageToApply;  } }

    private void Awake()
    {
        stateMachine = new();
    }

    public void Start()
    { 
        cosEnemyFOVover2InRAD = Mathf.Cos(EnemyFOV / 2f * Mathf.Deg2Rad); 
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
    
    protected internal bool SensePlayer()
    {        
        return EnemyUtilities.SenseOther(gameObject, player, cosEnemyFOVover2InRAD, closeEnoughSenseCutoff);
    }    

    protected internal bool EngagePlayer()
    {
        return EnemyUtilities.SenseOther(gameObject, player, cosEnemyFOVover2InRAD, closeEnoughEngageCutoff);
    }

    public void OnTriggerEnter(Collider other)
    {
        IDamageTaker damageTaker = other.gameObject.GetComponent<IDamageTaker>();
        damageTaker?.TakeDamage(DamageToApply);
    }

    //Abstract methods
    public abstract void Attack();
    public abstract void ApplyDamage(int damage);

    public abstract void TakeDamage(int damage);
}
