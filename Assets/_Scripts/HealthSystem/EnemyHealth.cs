using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthSystem
{
      public EnemyHealth() : base()
   {
     
   }

    // Update is called once per frame
    public override void IncreaseHealth()
    {
        //power up logic 
    }

    public override void Death(GameObject entity)
    {
        Debug.Log("Enemy dead....Enemy animation playingh......:(");
        entity.SetActive(false);
         
    }
    public override float ResetHealth()
    {
        return base.ResetHealth();
    }

}
