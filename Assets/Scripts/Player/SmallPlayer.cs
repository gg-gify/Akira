﻿using UnityEngine;
using System.Collections;

public class SmallPlayer : PlayerController
{
    private LightSource holdingLight;

    public override void BButtonPressed()
    {
        if (holdingLight != null)
        {
            holdingLight.BButtonPressed(this);
        }
    }

    public LightSource GetHoldingLight()
    {
        return holdingLight;
    }

    public void SetHoldingLight(LightSource holdingLight)
    {
        this.holdingLight = holdingLight;
        if (holdingLight != null)
        {
            holdingLight.SetTarget(transform);
        }
    }
}