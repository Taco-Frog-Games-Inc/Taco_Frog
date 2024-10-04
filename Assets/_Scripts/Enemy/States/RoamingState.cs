using System;
using UnityEngine;

public class RoamingState : EnemyStateMachine.State, IState
{
    public RoamingState(EnemyController controller, EnemyStateMachine stateMachine) {
        this.controller = controller;
        this.stateMachine = stateMachine;

        onEnter = OnEnter;
        onFrame = OnFrame;        
        onExit = OnExit;
    }

    public void OnEnter() { }
    public void OnFrame() {
        DoOnFrame();

        if (controller.health <= 0) stateMachine.ChangeState(EnemyStateMachine.StateEnum.DyingState);

        if (!controller.SensePlayer()) stateMachine.ChangeState(EnemyStateMachine.StateEnum.RoamingState);
        else stateMachine.ChangeState(EnemyStateMachine.StateEnum.ChasingState);
    }
    public void OnExit() { }


    public void DoOnEnter() { throw new Exception("DoOnEnter of RoamingState has yet to be implemented"); }
    public void DoOnFrame() {
        if (controller.path.transform.childCount == 0) throw new Exception("Insert waypoints");

        controller.navMeshAgent.destination = controller.path.transform.GetChild(controller.nextWayPointIndex).position;

        if (Vector3.Distance(controller.transform.position, controller.path.transform.GetChild(controller.nextWayPointIndex).position) < 1f)
            controller.nextWayPointIndex = UnityEngine.Random.Range(0, controller.path.transform.childCount);
    }
    public void DoOnExit() { throw new Exception("DoOnExit of RoamingState has yet to be implemented"); }    
}
