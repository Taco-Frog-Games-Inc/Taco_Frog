using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBH : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _env;

    [SerializeField] private float _health;
   PlayerDS _playerDS;
    HealthSystem _playerHS;
    //[SerializeField] private float timer = 0f;
    
    void Awake()
    {

    }
    
    void Start()
    {
        _enemy = GameObject.FindWithTag("Enemy");
        _env = GameObject.FindWithTag("Environment");
        _playerDS = new PlayerDS(_enemy,_env, "Hard");
        _playerHS = new PlayerHealth();
        _health = _playerHS.ResetHealth();
       // timer = 0f;
                
    }

    // Update is called once per frame
   /*void Update()
    {
        Vector3 enemyLoc = _enemy.transform.position;

        if(Vector3.Distance(enemyLoc, transform.position) < 2f)
        {
            // timer += Time.deltaTime;
             //if(timer > 3f)
            // {
                _health =  _playerDS.DamageTaken(_health); //update this as well
                
               // timer = 0f;
            // }
            if(_health < _playerHS.NearDeath())
            {
                _playerHS.Death(this.gameObject);
            }
            Debug.Log($"Health declined to :{_health}");
        }
    }
*/
    void DamageTaken()
    {
           _health =  _playerDS.DamageTaken(_health);  

            if(_health < _playerHS.NearDeath())_playerHS.Death(this.gameObject);

            Debug.Log($"Health declined to :{_health}");
    }
    void OnCollisionStay(Collision other)
    {
         if (other.gameObject.CompareTag("Enemy")) DamageTaken();
       
    }
}
