using System;
using System.Collections.Generic;
/*
 * Source File Name: EnemyStateMachine.cs
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
 *      This script provides functionality for an enemy state machine.
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 */
public class EnemyStateMachine { 
        public enum StateEnum {
            RoamingState,
            ChasingState,
            AttackingState,
            DyingState
        }

    /// <summary>
    /// Class delegating the onFrame, onEnter, and onExit actions to the enemy state machine.
    /// </summary>
    public abstract class State {
        protected internal EnemyController controller;
        protected internal EnemyStateMachine stateMachine;

        public string Name;

        public override string ToString() { return Name; }

        public Action onFrame;
        public Action onEnter;
        public Action onExit;        
    }

    private Dictionary<string, State> states = new();

    public State currentState { get; private set; }

    private State initialState;

    public State AddState(State state) { 
        state.Name = state.GetType().Name;

        if(states.Count == 0) initialState = state;

        states[state.Name] = state;

        return state;
    }

    public void FixedUpdate() {
        if (states.Count == 0) throw new Exception("*** State machine has no states!");

        if (currentState == null) ChangeState(initialState);

        currentState.onFrame?.Invoke();        
    }

    public void ChangeState(State state) {
        if (currentState != null && currentState.onExit != null) currentState.onExit();

        currentState = state ?? throw new Exception("*** Cannot change to a null state ***");

        currentState.onEnter?.Invoke();
    }

    public void ChangeState(StateEnum newStateEnum)
    {
        if (!states.ContainsKey(newStateEnum.ToString())) throw new Exception($"*** State machine does not have the state {newStateEnum} ***");

        ChangeState(states[newStateEnum.ToString()]);
    }
}
