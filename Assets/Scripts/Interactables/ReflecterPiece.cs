using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflecterPiece : Emitter
{
    public override void InteractX(PlayerController player){}

    public void RecivingLight(Color lineColor)
    {
        DrawLine(lineColor);
    }

    public void TurnOffLight()
    {
        EraseLine();
    }
}
