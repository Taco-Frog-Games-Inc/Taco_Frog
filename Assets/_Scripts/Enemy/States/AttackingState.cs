using UnityEngine;
/*
 * Source File Name: AttackingState.cs
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
 *      This script provides functionality for the attacking state.
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 *      -> October 12th, 2024:
 *          -Added StopAttack() in the DoOnExit and moved Attack() in the DoOnEnter();
 *      -> October 16th, 2024:
 *          -Added Attacking to Roaming transition
 *      -> November 10th, 2024:
 *          -Adjusted the SensePlayer and EngagePlayer calls.
 */
public class AttackingState : EnemyStateMachine.State, IState
{
    private (GameObject, bool) playerSenseTuple;
    private (GameObject, bool) playerEngageTuple;
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
    public void OnEnter() { DoOnEnter(); }

    /// <summary>
    /// Executes once per frame.
    /// Changes to the proper state based on fixed conditions.
    /// </summary>
    public void OnFrame()
    {
        playerSenseTuple = controller.SensePlayer();
        playerEngageTuple = controller.EngagePlayer();
        DoOnFrame();

        if (controller.Health <= 0) stateMachine.ChangeState(EnemyStateMachine.StateEnum.DyingState);

        if (!playerEngageTuple.Item2 && playerSenseTuple.Item2) stateMachine.ChangeState(EnemyStateMachine.StateEnum.ChasingState);
        else if (!playerEngageTuple.Item2 && !playerSenseTuple.Item2) stateMachine.ChangeState(EnemyStateMachine.StateEnum.RoamingState);
    }

    /// <summary>
    /// Executes once upon completion of the attacking state.
    /// </summary>
    public void OnExit() { DoOnExit(); }

    /// <summary>
    /// Calls the controller's attack functionality.
    /// </summary>
    public void DoOnEnter() { 
        controller.Attack();
    }

    /// <summary>
    /// Ensures the controller follows the player.
    /// </summary>
    public void DoOnFrame() { 
        if(playerSenseTuple.Item2)
            controller.navMeshAgent.destination = playerSenseTuple.Item1.transform.position; 
    }

    /// <summary>
    /// Stops the controller's attack functionality. 
    /// </summary>
    public void DoOnExit() {  controller.StopAttack(); }
}
