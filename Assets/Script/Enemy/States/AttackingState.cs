using System;
using UnityEngine;

public class AttackingState : EnemyStateMachine.State, IState
{
    public AttackingState(EnemyController controller, EnemyStateMachine stateMachine)
    {
        this.controller = controller;
        this.stateMachine = stateMachine;

        onEnter = OnEnter;
        onFrame = OnFrame;
        onExit = OnExit;
    }

    public void OnEnter() { 
        controller.initialColor = controller.GetComponent<Renderer>().material.color;
    }
    public void OnFrame()
    {
        DoOnFrame();

        if (controller.health <= 0) stateMachine.ChangeState(EnemyStateMachine.StateEnum.DyingState);

        if (!controller.EngagePlayer()) stateMachine.ChangeState(EnemyStateMachine.StateEnum.ChasingState);

    }
    public void OnExit() {
        controller.GetComponent<Renderer>().material.color = controller.initialColor;
    }


    public void DoOnEnter() { throw new Exception("DoOnEnter of AttackingState has yet to be implemented"); }
    public void DoOnFrame() {
        controller.navMeshAgent.destination = controller.player.transform.position;
        controller.Attack();
    }
    public void DoOnExit() { throw new Exception("DoOnExit of AttackingState has yet to be implemented"); }
}
