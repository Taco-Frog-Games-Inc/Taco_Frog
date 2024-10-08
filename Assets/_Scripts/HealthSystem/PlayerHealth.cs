using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerHealth : HealthSystem
{
   
     public PlayerHealth() : base()
   {
     
   }

    public override void IncreaseHealth()
    {
        //power up logic 
    }

    public override void Death(GameObject entity)
    {
        Debug.Log("PLayer dead....Player animation playingh......:(");
        entity.SetActive(false);
         
    }
    public override float ResetHealth()
    {
        return base.ResetHealth();
    }

   
}
