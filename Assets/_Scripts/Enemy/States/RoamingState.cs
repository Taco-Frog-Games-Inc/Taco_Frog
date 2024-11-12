using System;
using UnityEngine;
/*
 * Source File Name: RoamingState.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: October 2nd, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: November 10th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script provides functionality for the roaming state.
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 *      -> November 10th, 2024:
 *          -Adjusted the SensePlayer and EngagePlayer calls.
 */
public class RoamingState : EnemyStateMachine.State, IState
{
    public RoamingState(EnemyController controller, EnemyStateMachine stateMachine) {
        this.controller = controller;
        this.stateMachine = stateMachine;

        onEnter = OnEnter;
        onFrame = OnFrame;        
        onExit = OnExit;
    }

    /// <summary>
    /// Executes once when the roaming state has been entered.    
    /// </summary>
    public void OnEnter() { }

    /// <summary>
    /// Executes once per frame.
    /// Changes to the proper state based on fixed conditions.
    /// </summary>
    public void OnFrame() {
        DoOnFrame();

        if (controller.Health <= 0) stateMachine.ChangeState(EnemyStateMachine.StateEnum.DyingState);

        if (!controller.SensePlayer().Item2) stateMachine.ChangeState(EnemyStateMachine.StateEnum.RoamingState);
        else stateMachine.ChangeState(EnemyStateMachine.StateEnum.ChasingState);
    }


    /// <summary>
    /// Executes once upon completion of the roaming state.
    /// </summary>
    public void OnExit() { }


    public void DoOnEnter() { throw new Exception("DoOnEnter of RoamingState has yet to be implemented"); }

    /// <summary>
    /// Sets the controller's next destination point.
    /// </summary>
    public void DoOnFrame() {
        if (controller.path.transform.childCount == 0) throw new Exception("Insert waypoints");

        controller.navMeshAgent.destination = controller.path.transform.GetChild(controller.nextWayPointIndex).position;

        if (Vector3.Distance(controller.transform.position, controller.path.transform.GetChild(controller.nextWayPointIndex).position) < 1f) {
            controller.nextWayPointIndex = UnityEngine.Random.Range(0, controller.path.transform.childCount);
        }
        
    }
    public void DoOnExit() { throw new Exception("DoOnExit of RoamingState has yet to be implemented"); }    
}
