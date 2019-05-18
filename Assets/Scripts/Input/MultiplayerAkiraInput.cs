using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerAkiraInput : AkiraInput
{
    private bool buttonA;
    private bool buttonB;
    private Vector2 pad;

    public override bool GetButtonA()
    {
        return buttonA;
    }

    public override bool GetButtonB()
    {
        return buttonB;
    }

    public override Vector2 GetPad()
    {
        return pad;
    }

    public void SetButtonA(bool button)
    {
        buttonA = button;
    }

    public void SetButtonB(bool button)
    {
        buttonB = button;
    }

    public void SetPad(Vector2 axis)
    {
        pad = axis;
    }
}
