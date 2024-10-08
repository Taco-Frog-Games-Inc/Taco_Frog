using System.Linq;
using NUnit.Framework;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


public class PlayerInputTest : InputTestFixture
{
    // Test player input
    [Test]
    public void Player_Jump_Input_Test()
    {
        // Use the Assert class to test conditions
        var keyboard = InputSystem.AddDevice<Keyboard>();

        var jump = new InputAction("Jump", InputActionType.Button, binding: "<Keyboard>/leftShift", interactions: "Hold");



        jump.Enable();

        Press(keyboard.leftShiftKey);

        using (var trace = new InputActionTrace())
        {
            trace.SubscribeTo(jump);

            jump.Disable();

            var actions = trace.ToArray();

            Assert.That(actions.Length, Is.EqualTo(1));
        }
    }

    // Test player movement input
    [Test]
    public void Player_Movement_Input_Test()
    {
        // Use the Assert class to test conditions
        var keyboard = InputSystem.AddDevice<Keyboard>();

        var w = new InputAction("W", InputActionType.Button, binding: "<Keyboard>/w", interactions: "Hold");
        var a = new InputAction("A", InputActionType.Button, binding: "<Keyboard>/a", interactions: "Hold");
        var s = new InputAction("S", InputActionType.Button, binding: "<Keyboard>/a", interactions: "Hold");
        var d = new InputAction("D", InputActionType.Button, binding: "<Keyboard>/d", interactions: "Hold");

        w.Enable();
        a.Enable();
        s.Enable();
        d.Enable();

        Press(keyboard.wKey);
        Press(keyboard.aKey);
        Press(keyboard.sKey);
        Press(keyboard.dKey);

        using (var trace = new InputActionTrace())
        {
            trace.SubscribeTo(w);
            trace.SubscribeTo(a);
            trace.SubscribeTo(s);
            trace.SubscribeTo(d);

            w.Disable();
            a.Disable();
            s.Disable();
            d.Disable();

            var actions = trace.ToArray();

            Assert.That(actions.Length, Is.EqualTo(4));
        }
    }
}
