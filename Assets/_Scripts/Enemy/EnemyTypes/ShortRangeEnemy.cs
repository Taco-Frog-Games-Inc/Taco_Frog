using UnityEngine;

public class ShortRangeEnemy : EnemyController
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        stateMachine.AddState(new RoamingState(this, stateMachine));
        stateMachine.AddState(new ChasingState(this, stateMachine));
        stateMachine.AddState(new AttackingState(this, stateMachine));
        stateMachine.AddState(new DyingState(this, stateMachine));
    }

    public override void Attack() {
        GetComponent<Renderer>().material.color = Color.red;

        //This should occur only when the conditions for hurting a player has been met.         
        ApplyDamage(1);
    }    

    public override void ApplyDamage(int damage)
    {
        Debug.Log("Applying Damage to player: " + damage);
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
    }
}
