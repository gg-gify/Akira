using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardAkiraInput : AkiraInput
{

    private const KeyCode KEYBOARD_A_BUTTON = KeyCode.H;
    private const KeyCode KEYBOARD_B_BUTTON = KeyCode.U;
    private const KeyCode KEYBOARD_UP_BUTTON = KeyCode.W;
    private const KeyCode KEYBOARD_LEFT_BUTTON = KeyCode.A;
    private const KeyCode KEYBOARD_DOWN_BUTTON = KeyCode.S;
    private const KeyCode KEYBOARD_RIGHT_BUTTON = KeyCode.D;

    public override bool GetButtonA()
    {
        return Input.GetKey(KEYBOARD_A_BUTTON);
    }

    public override bool GetButtonB()
    {
        return Input.GetKey(KEYBOARD_B_BUTTON);
    }

    public override Vector2 GetPad()
    {
        bool upKey = Input.GetKey(KEYBOARD_UP_BUTTON);
        bool leftKey = Input.GetKey(KEYBOARD_LEFT_BUTTON);
        bool downKey = Input.GetKey(KEYBOARD_DOWN_BUTTON);
        bool rightKey = Input.GetKey(KEYBOARD_RIGHT_BUTTON);

        Vector2 pad = Vector2.zero;
        pad.x += (rightKey ? 1 : 0);
        pad.x -= (leftKey ? 1 : 0);
        pad.y += (upKey ? 1 : 0);
        pad.y -= (downKey ? 1 : 0);

        return pad;
    }
}
