using UnityEngine;

public abstract class HealthSystem 
{
    protected readonly float fullHealth;
    protected readonly float death = 1f;

    public HealthSystem() {
        fullHealth = 100f;
    }

    public virtual float ResetHealth()
    {
       return fullHealth;
    }
    public virtual void IncreaseHealth(){}

    public virtual void Death(GameObject entity)
    {
        
    }

    public virtual float NearDeath()
    {
        return death;
    }
}
