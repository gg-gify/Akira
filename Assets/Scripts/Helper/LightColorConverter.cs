using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorConverter : MonoBehaviour
{
    public static Color GetColor(LightColor light)
    {
        switch (light)
        {
            case LightColor.White:
                return Color.white;
            case LightColor.Red:
                return Color.red;
        }
        return Color.white;
    }
}

public enum LightColor
{
    White,
    Red
}
