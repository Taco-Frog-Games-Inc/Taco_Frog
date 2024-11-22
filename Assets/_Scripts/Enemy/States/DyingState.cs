using System;
using System.Diagnostics;
using UnityEngine.SceneManagement;
/*
 * Source File Name: DyingState.cs
 * Author Name: Audrey Bernier Larose
 * Student Number: 301166198
 * Creation Date: October 2nd, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: October 2nd, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script provides functionality for the dying state.
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 */
public class DyingState : EnemyStateMachine.State, IState
{
    public DyingState(EnemyController controller, EnemyStateMachine stateMachine)
    {
        this.controller = controller;
        this.stateMachine = stateMachine;

        onEnter = OnEnter;
        onFrame = OnFrame;
        onExit = OnExit;
    }

    /// <summary>
    /// Executes once when the dying state has been entered.    
    /// </summary>
    public void OnEnter() { }

    /// <summary>
    /// Executes once per frame.
    /// </summary>
    public void OnFrame() { DoOnFrame(); }

    /// <summary>
    /// Executes once upon completion of the dying state.
    /// </summary>
    public void OnExit() { }


    public void DoOnEnter() { throw new Exception("DoOnEnter of DyingState has yet to be implemented"); }
    public void DoOnFrame() { 
        //Destroy player after a certain amount of seconds...
    }
    public void DoOnExit() { throw new Exception("DoOnExit of DyingState has yet to be implemented"); }
}
