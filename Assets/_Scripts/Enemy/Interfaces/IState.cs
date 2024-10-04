
public interface IState
{
    void OnFrame();
    void OnEnter();
    void OnExit();

    void DoOnFrame();
    void DoOnEnter();
    void DoOnExit();
}