using UnityEngine;
/*
 * Source File Name: AttackingState.cs
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
 *      This script provides functionality for the attacking state.
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 *      -> October 12th, 2024:
 *          -Added StopAttack() in the DoOnExit and moved Attack() in the DoOnEnter();
 */
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

    /// <summary>
    /// Executes once when the attacking state has been entered.    
    /// </summary>
    public void OnEnter() {
        controller.initialColor = controller.GetComponent<Renderer>().material.color;
        DoOnEnter();
    }

    /// <summary>
    /// Executes once per frame.
    /// Changes to the proper state based on fixed conditions.
    /// </summary>
    public void OnFrame()
    {
        DoOnFrame();

        if (controller.Health <= 0) stateMachine.ChangeState(EnemyStateMachine.StateEnum.DyingState);

        if (!controller.EngagePlayer()) stateMachine.ChangeState(EnemyStateMachine.StateEnum.ChasingState);

    }

    /// <summary>
    /// Executes once upon completion of the attacking state.
    /// </summary>
    public void OnExit() {
        controller.GetComponent<Renderer>().material.color = controller.initialColor;
        DoOnExit();
    }

    /// <summary>
    /// Calls the controller's attack functionality.
    /// </summary>
    public void DoOnEnter() { 
        controller.Attack();
        controller.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    /// <summary>
    /// Ensures the controller follows the player.
    /// </summary>
    public void DoOnFrame() { controller.navMeshAgent.destination = controller.player.transform.position; }

    /// <summary>
    /// Stops the controller's attack functionality. 
    /// </summary>
    public void DoOnExit() { 
        controller.StopAttack();
        controller.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }
}
