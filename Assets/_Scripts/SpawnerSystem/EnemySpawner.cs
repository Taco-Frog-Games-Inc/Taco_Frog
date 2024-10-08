using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SpawnManager 
{
      
    private int enemyCount = 10;          
    


    public EnemySpawner(GameObject entity, LayerMask layerMask,  Vector3 mapSize)
    :base(entity,layerMask, mapSize )
    {
        SpawnGameObjects(enemyCount);
    }  

   
    public  override void  SpawnGameObjects(int entityCount)
    {
        base.SpawnGameObjects(entityCount);
    }
}
