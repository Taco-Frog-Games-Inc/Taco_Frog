using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotSuacePickup : SpawnManager
{
    private int itemCount = 2;          
    
    

    public HotSuacePickup(GameObject entity, LayerMask layerMask,  Vector3 mapSize, bool item)
    :base(entity,layerMask, mapSize, item)
    {
        
        SpawnGameItems(itemCount);
    }  

   
    public  override void  SpawnGameItems(int entityCount)
    {
        base.SpawnGameObjects(entityCount);
    }
}
