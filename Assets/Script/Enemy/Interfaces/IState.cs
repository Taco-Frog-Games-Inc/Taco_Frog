using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnFrame();
    void OnEnter();
    void OnExit();

    void DoOnFrame();
    void DoOnEnter();
    void DoOnExit();
}
