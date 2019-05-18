using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickAkiraInput : AkiraInput
{
    private const string HORIZONTAL_PAD_JOYSTICK_1 = "HorizontalPad1";
    private const string VERTICAL_PAD_JOYSTICK_1 = "VerticalPad1";
    private const KeyCode JOYSTCIK_1_A_BUTTON = KeyCode.Joystick1Button0;
    private const KeyCode JOYSTCIK_1_B_BUTTON = KeyCode.Joystick1Button1;

    private const string HORIZONTAL_PAD_JOYSTICK_2 = "HorizontalPad2";
    private const string VERTICAL_PAD_JOYSTICK_2 = "VerticalPad2";
    private const KeyCode JOYSTCIK_2_A_BUTTON = KeyCode.Joystick2Button0;
    private const KeyCode JOYSTCIK_2_B_BUTTON = KeyCode.Joystick2Button1;

    private readonly int joystcikID;

    public JoystickAkiraInput(int joystcikID)
    {
        this.joystcikID = joystcikID;
    }

    public override bool GetButtonA()
    {
        switch (joystcikID)
        {
            case 2:
                return Input.GetKeyDown(JOYSTCIK_2_A_BUTTON);
            default:
                return Input.GetKeyDown(JOYSTCIK_1_A_BUTTON);
        }
    }

    public override bool GetButtonB()
    {
        switch (joystcikID)
        {
            case 2:
                return Input.GetKeyDown(JOYSTCIK_2_B_BUTTON);
            default:
                return Input.GetKeyDown(JOYSTCIK_1_B_BUTTON);
        }
    }

    public override Vector2 GetPad()
    {
        switch (joystcikID)
        {
            case 2:
                return new Vector2(Input.GetAxisRaw(HORIZONTAL_PAD_JOYSTICK_2), Input.GetAxisRaw(VERTICAL_PAD_JOYSTICK_2));
            default:
                return new Vector2(Input.GetAxisRaw(HORIZONTAL_PAD_JOYSTICK_1), Input.GetAxisRaw(VERTICAL_PAD_JOYSTICK_1));
        }
    }
}
