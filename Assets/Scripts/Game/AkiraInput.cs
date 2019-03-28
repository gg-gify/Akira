using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AkiraInput
{
    private const string HORIZONTAL_PAD_JOYSTICK_1 = "HorizontalPad1";
    private const string VERTICAL_PAD_JOYSTICK_1 = "VerticalPad1";
    private const string HORIZONTAL_PAD_JOYSTICK_2 = "HorizontalPad2";
    private const string VERTICAL_PAD_JOYSTICK_2 = "VerticalPad2";
    private const string HORIZONTAL_PAD_KEYBOARD = "HorizontalKeyboard";
    private const string VERTICAL_PAD_KEYBOARD = "VerticalKeyboard";
    private const KeyCode JOYSTCIK_1_A_BUTTON = KeyCode.Joystick1Button0;
    private const KeyCode JOYSTCIK_1_B_BUTTON = KeyCode.Joystick1Button1;
    private const KeyCode JOYSTCIK_2_A_BUTTON = KeyCode.Joystick2Button0;
    private const KeyCode JOYSTCIK_2_B_BUTTON = KeyCode.Joystick2Button1;
    private const KeyCode KEYBOARD_A_BUTTON = KeyCode.H;
    private const KeyCode KEYBOARD_B_BUTTON = KeyCode.U;
    private const KeyCode KEYBOARD_UP_BUTTON = KeyCode.W;
    private const KeyCode KEYBOARD_LEFT_BUTTON = KeyCode.A;
    private const KeyCode KEYBOARD_DOWN_BUTTON = KeyCode.S;
    private const KeyCode KEYBOARD_RIGHT_BUTTON = KeyCode.D;

    public static Vector2 GetPad(AkiraInputModel model)
    {
        bool upKey = false;
        bool leftKey = false;
        bool downKey = false;
        bool rightKey = false;
        switch (model)
        {
            case AkiraInputModel.Joystick1:
                upKey = Input.GetAxisRaw(VERTICAL_PAD_JOYSTICK_1) > 0;
                leftKey = Input.GetAxisRaw(HORIZONTAL_PAD_JOYSTICK_1) < 0;
                downKey = Input.GetAxisRaw(VERTICAL_PAD_JOYSTICK_1) < 0;
                rightKey = Input.GetAxisRaw(HORIZONTAL_PAD_JOYSTICK_1) > 0;
                break;
            case AkiraInputModel.Joystick2:
                upKey = Input.GetAxisRaw(VERTICAL_PAD_JOYSTICK_2) > 0;
                leftKey = Input.GetAxisRaw(HORIZONTAL_PAD_JOYSTICK_2) < 0;
                downKey = Input.GetAxisRaw(VERTICAL_PAD_JOYSTICK_2) < 0;
                rightKey = Input.GetAxisRaw(HORIZONTAL_PAD_JOYSTICK_2) > 0;
                break;
            case AkiraInputModel.Keyboard:
                upKey = Input.GetKey(KEYBOARD_UP_BUTTON);
                leftKey = Input.GetKey(KEYBOARD_LEFT_BUTTON);
                downKey = Input.GetKey(KEYBOARD_DOWN_BUTTON);
                rightKey = Input.GetKey(KEYBOARD_RIGHT_BUTTON);
                break;
        }
        Vector2 pad = Vector2.zero;
        pad.x += (rightKey ? 1 : 0);
        pad.x -= (leftKey ? 1 : 0);
        pad.y += (upKey ? 1 : 0);
        pad.y -= (downKey ? 1 : 0);
        return pad;
    }

    public static bool GetButtonA(AkiraInputModel model)
    {
        bool button = false;
        switch (model)
        {
            case AkiraInputModel.Joystick1:
                button = Input.GetKey(JOYSTCIK_1_A_BUTTON);
                break;
            case AkiraInputModel.Joystick2:
                button = Input.GetKey(JOYSTCIK_2_A_BUTTON);
                break;
            case AkiraInputModel.Keyboard:
                button = Input.GetKey(KEYBOARD_A_BUTTON);
                break;
        }
        return button;
    }

    public static bool GetButtonB(AkiraInputModel model)
    {
        bool button = false;
        switch (model)
        {
            case AkiraInputModel.Joystick1:
                button = Input.GetKey(JOYSTCIK_1_B_BUTTON);
                break;
            case AkiraInputModel.Joystick2:
                button = Input.GetKey(JOYSTCIK_2_B_BUTTON);
                break;
            case AkiraInputModel.Keyboard:
                button = Input.GetKey(KEYBOARD_B_BUTTON);
                break;
        }
        return button;
    }
}

public enum AkiraInputModel
{
    Joystick1,
    Joystick2,
    Keyboard
}