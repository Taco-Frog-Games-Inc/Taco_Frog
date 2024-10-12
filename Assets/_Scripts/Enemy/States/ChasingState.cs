using System;

public class ChasingState : EnemyStateMachine.State, IState
{
    public ChasingState(EnemyController controller, EnemyStateMachine stateMachine)
    {
        this.controller = controller;
        this.stateMachine = stateMachine;

        onEnter = OnEnter;
        onFrame = OnFrame;
        onExit = OnExit;
    }

    public void OnEnter() { DoOnEnter(); }
    public void OnFrame()
    {
        DoOnFrame();

        if (controller.Health <= 0) stateMachine.ChangeState(EnemyStateMachine.StateEnum.DyingState);

        if (!controller.SensePlayer()) stateMachine.ChangeState(EnemyStateMachine.StateEnum.RoamingState);
        else if (controller.EngagePlayer()) stateMachine.ChangeState(EnemyStateMachine.StateEnum.AttackingState);

    }
    public void OnExit() { DoOnExit(); }


    public void DoOnEnter() { 
        controller.navMeshAgent.speed *= 2f; //Increases the speed by a factor of 1.2 when chasing.
    }
    public void DoOnFrame() { 
        controller.navMeshAgent.destination = controller.player.transform.position;        
    }
    public void DoOnExit() {
        controller.navMeshAgent.speed /= 2f; //Decreases the speed by a factor of 1.2 when exiting chasing phase.
    }
}

