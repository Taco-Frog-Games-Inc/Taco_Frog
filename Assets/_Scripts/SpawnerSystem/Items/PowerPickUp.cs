using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickUp : SpawnManager
{
   private int itemCount = 7;          
    
    

    public PowerPickUp(GameObject entity, LayerMask layerMask,  Vector3 mapSize, bool item)
    :base(entity,layerMask, mapSize, item)
    {
        
        SpawnGameItems(itemCount);
    }  

   
    public  override void  SpawnGameItems(int entityCount)
    {
        base.SpawnGameObjects(entityCount);
    }
}
