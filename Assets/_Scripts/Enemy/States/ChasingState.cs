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

    public void OnEnter() { }
    public void OnFrame()
    {
        DoOnFrame();

        if (controller.health <= 0) stateMachine.ChangeState(EnemyStateMachine.StateEnum.DyingState);

        if (!controller.SensePlayer()) stateMachine.ChangeState(EnemyStateMachine.StateEnum.RoamingState);
        else if (controller.EngagePlayer()) stateMachine.ChangeState(EnemyStateMachine.StateEnum.AttackingState);

    }
    public void OnExit() { }


    public void DoOnEnter() { throw new Exception("DoOnEnter of ChasingState has yet to be implemented"); }
    public void DoOnFrame() { controller.navMeshAgent.destination = controller.player.transform.position; }
    public void DoOnExit() { throw new Exception("DoOnExit of ChasingState has yet to be implemented"); }
}

