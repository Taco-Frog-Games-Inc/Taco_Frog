using System;

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

    public void OnEnter() { }
    public void OnFrame()
    {
        DoOnFrame();
    }
    public void OnExit() { }


    public void DoOnEnter() { throw new Exception("DoOnEnter of DyingState has yet to be implemented"); }
    public void DoOnFrame() { 
        //Destroy player after a certain amount of seconds...
    }
    public void DoOnExit() { throw new Exception("DoOnExit of DyingState has yet to be implemented"); }
}
