/*
 * Source File Name: IState.cs
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
 *      This script provides a contract for states to be included in the state machine.
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 */
public interface IState
{
    void OnFrame();
    void OnEnter();
    void OnExit();

    void DoOnFrame();
    void DoOnEnter();
    void DoOnExit();
}