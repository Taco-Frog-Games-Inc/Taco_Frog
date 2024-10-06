using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [Header("Enemy Spawn Station")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Vector3 mapSize;



    SpawnManager eS;
   
   void Awake()
   {
        mapSize = GameObject.FindWithTag("Floor").GetComponent<Transform>().lossyScale;
       
        Debug.Log($"mapSize: {mapSize}");
        eS = new EnemySpawner(enemyPrefab, groundMask, mapSize);
   }
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
