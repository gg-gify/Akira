using UnityEngine;
using UnityEditor;
using System;

public class Beacon : Emitter
{
    private const float MAX_BEACON_BATTERY = 6f;

    [SerializeField] private float maxLineWidth = 0.2f;

    private LightSource currentLight;
    private float currentBatteryStatus;

    protected override void Update()
    {
        base.Update();
        if (IsEmittingLight())
        {
            // currentBatteryStatus -= Time.deltaTime;
            if (currentBatteryStatus <= 0)
            {
                ShutdownLight();
                currentLight.SetTarget(null);
                SetCurrentLight(null);
            }
            currentLineWidth = maxLineWidth * (currentBatteryStatus / MAX_BEACON_BATTERY);
        }
    }

    public void SetCurrentLight(LightSource currentLight)
    {
        this.currentLight = currentLight;
        if (currentLight != null)
        {
            currentLight.SetTarget(transform);
        }
    }

    public override void AButtonPressed(PlayerController player)
    {
        if (player is SmallPlayer)
        {
            SmallPlayer currentPlayer = (SmallPlayer)player;
            if (currentPlayer.GetHoldingLight() != null)
            {
                LightSource tmp = currentLight;
                SetCurrentLight(currentPlayer.GetHoldingLight());
                currentPlayer.SetHoldingLight(tmp);
            }
            else if (currentLight != null)
            {
                currentPlayer.SetHoldingLight(currentLight);
                SetCurrentLight(null);
            }

            if (currentLight != null)
            {
                EmitLight(currentLight.GetColor(), maxLineWidth);
                currentBatteryStatus = MAX_BEACON_BATTERY;
            }
            else
            {
                ShutdownLight();
            }
        }
    }

    public override void BButtonPressed(PlayerController player) { }
}