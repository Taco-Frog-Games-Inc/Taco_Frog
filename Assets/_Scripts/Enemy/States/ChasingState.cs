/*
 * Source File Name: ChasingState.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: October 2nd, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: October 12th, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script provides functionality for the chasing state.
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 *      -> October 12th, 2024:
 *          -Increased the object's speed on enter and decreased it back to its initial speed on exit.
 */
using UnityEngine;

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

    /// <summary>
    /// Executes once when the chasing state has been entered.
    /// </summary>
    public void OnEnter() { DoOnEnter(); }

    /// <summary>
    /// Executes once per frame.
    /// Changes to the proper state based on fixed conditions.
    /// </summary>
    public void OnFrame()
    {
        DoOnFrame();

        if (controller.Health <= 0) stateMachine.ChangeState(EnemyStateMachine.StateEnum.DyingState);

        if (!controller.SensePlayer()) stateMachine.ChangeState(EnemyStateMachine.StateEnum.RoamingState);
        else if (controller.EngagePlayer()) stateMachine.ChangeState(EnemyStateMachine.StateEnum.AttackingState);

    }

    /// <summary>
    /// Executes once upon completion of the chasing state.
    /// </summary>
    public void OnExit() { DoOnExit(); }

    /// <summary>
    /// Increases the speed of the controller by a factor of 2.
    /// </summary>
    public void DoOnEnter() { 
        controller.navMeshAgent.speed *= 2f;
       //controller.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    /// <summary>
    /// Ensures the controller follows the player.
    /// </summary>
    public void DoOnFrame() { controller.navMeshAgent.destination = controller.player.transform.position; }

    /// <summary>
    /// Decreases the speed of the controller by a factor of 2.
    /// </summary>
    public void DoOnExit() { 
        controller.navMeshAgent.speed /= 2f;
        //controller.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }
}

