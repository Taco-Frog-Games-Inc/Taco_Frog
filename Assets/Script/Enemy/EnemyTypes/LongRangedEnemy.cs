
using UnityEngine;

public class LongRangedEnemy : EnemyController
{
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
    }
}
