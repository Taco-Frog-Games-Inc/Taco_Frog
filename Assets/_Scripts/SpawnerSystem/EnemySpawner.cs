using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SpawnManager 
{
      
    private int enemyCount = 1;          
    

    public EnemySpawner() : base()
    {

    }
    public EnemySpawner(GameObject entity, LayerMask layerMask,  Vector3 mapSize, bool item )
    :base(entity,layerMask, mapSize, item)
    {
        
        SpawnGameObjects(enemyCount);
        Debug.Log("Called!");
    }  

   
    public  override void  SpawnGameObjects(int entityCount)
    {
        base.SpawnGameObjects(entityCount);
    }
}
