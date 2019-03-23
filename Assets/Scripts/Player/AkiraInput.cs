using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AkiraInput
{
    private const string HORIZONTAL_AXIS_JOYSTICK_1 = "HorizontalPlayer1";
    private const string VERTICAL_AXIS_JOYSTICK_1 = "VerticalPlayer1";
    private const string HORIZONTAL_AXIS_JOYSTICK_2 = "HorizontalPlayer2";
    private const string VERTICAL_AXIS_JOYSTICK_2 = "VerticalPlayer2";
    private const string HORIZONTAL_AXIS_KEYBOARD = "HorizontalKeyboard";
    private const string VERTICAL_AXIS_KEYBOARD = "VerticalKeyboard";
    private const KeyCode JOYSTCIK_1_X_BUTTON = KeyCode.Joystick1Button0;
    private const KeyCode JOYSTCIK_1_Y_BUTTON = KeyCode.Joystick1Button1;
    private const KeyCode JOYSTCIK_2_X_BUTTON = KeyCode.Joystick2Button0;
    private const KeyCode JOYSTCIK_2_Y_BUTTON = KeyCode.Joystick2Button1;
    private const KeyCode KEYBOARD_X_BUTTON = KeyCode.H;
    private const KeyCode KEYBOARD_Y_BUTTON = KeyCode.U;

    public static Vector2 GetMovimentAxis(AkiraInputModel model)
    {
        Vector2 axis = Vector2.zero;
        switch (model)
        {
            case AkiraInputModel.Joystick1:
                axis = new Vector2(Input.GetAxisRaw(HORIZONTAL_AXIS_JOYSTICK_1), Input.GetAxisRaw(VERTICAL_AXIS_JOYSTICK_1));
                break;
            case AkiraInputModel.Joystick2:
                axis = new Vector2(Input.GetAxisRaw(HORIZONTAL_AXIS_JOYSTICK_2), Input.GetAxisRaw(VERTICAL_AXIS_JOYSTICK_2));
                break;
            case AkiraInputModel.Keyboard:
                axis = new Vector2(Input.GetAxisRaw(HORIZONTAL_AXIS_KEYBOARD), Input.GetAxisRaw(VERTICAL_AXIS_KEYBOARD));
                break;
        }
        return axis;
    }

    public static bool GetButtonX(AkiraInputModel model)
    {
        bool button = false;
        switch (model)
        {
            case AkiraInputModel.Joystick1:
                button = Input.GetKeyDown(JOYSTCIK_1_X_BUTTON);
                break;
            case AkiraInputModel.Joystick2:
                button = Input.GetKeyDown(JOYSTCIK_2_X_BUTTON);
                break;
            case AkiraInputModel.Keyboard:
                button = Input.GetKeyDown(KEYBOARD_X_BUTTON);
                break;
        }
        return button;
    }

    public static bool GetButtonY(AkiraInputModel model)
    {
        bool button = false;
        switch (model)
        {
            case AkiraInputModel.Joystick1:
                button = Input.GetKeyDown(JOYSTCIK_1_Y_BUTTON);
                break;
            case AkiraInputModel.Joystick2:
                button = Input.GetKeyDown(JOYSTCIK_2_Y_BUTTON);
                break;
            case AkiraInputModel.Keyboard:
                button = Input.GetKeyDown(KEYBOARD_Y_BUTTON);
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
