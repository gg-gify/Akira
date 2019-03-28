using UnityEngine;
using UnityEditor;
using System;

public class Beacon : Emitter
{
    private const float MAX_BEACON_BATTERY = 6f;

    [SerializeField] private float beaconInteractionCooldown = 1f;
    [SerializeField] private float maxLineWidth = 0.2f;

    private LightSource currentLight;
    private float currentBatteryStatus;
    private bool isOnCooldown;
    private float currentCooldownTime;

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
        if (isOnCooldown)
        {
            currentCooldownTime -= Time.deltaTime;
            if (currentCooldownTime <= 0)
            {
                isOnCooldown = false;
            }
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
        if (isOnCooldown) return;
        if (player.GetHoldingLight() != null)
        {
            LightSource tmp = currentLight;
            SetCurrentLight(player.GetHoldingLight());
            player.SetHoldingLight(tmp);
        }
        else if (currentLight != null)
        {
            player.SetHoldingLight(currentLight);
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
        isOnCooldown = true;
        currentCooldownTime = beaconInteractionCooldown;
    }
}