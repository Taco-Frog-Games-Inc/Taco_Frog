using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageSystem : IDamage
{
    protected readonly GameObject _entity;

    public DamageSystem(GameObject entity)
    {
        entity = _entity;
    }
   
   public virtual float DamageTaken(float damage){ return damage;}
}
