﻿using UnityEngine;
using UnityEditor;
using System;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(ParticleSystem))]
public class LightSource : Interactable
{
    [SerializeField] LightSourceColor color;

    private Vector3 startPosition;
    private Transform target;
    private ParticleSystem lightParticles;
    private Light unityLight;

    private void Start()
    {
        startPosition = transform.position;
        lightParticles = GetComponent<ParticleSystem>();
        GetComponent<ParticleSystemRenderer>().material.color = GetColor();
        unityLight = GetComponent<Light>();
        unityLight.color = GetColor();
    }

    protected override void Update()
    {
        if (target != null)
        {
            transform.position = target.position;

        }
        else
        {
            transform.position = startPosition;
            base.Update();
        }
    }
    
    protected override void OnDrawGizmosSelected()
    {
        if (target == null)
        {
            base.OnDrawGizmosSelected();
        }
    }

    public Color GetColor()
    {
        return LightSourceColorConverter.GetColor(color);
    }

    public override void AButtonPressed(PlayerController player)
    {
        if (player is SmallPlayer)
        {
            SmallPlayer currentPlayer = (SmallPlayer)player;
            currentPlayer.SetHoldingLight(this);
            currentPlayer.SetInteraction(null);
        }
    }

    public override void BButtonPressed(PlayerController player)
    {
        if (player is SmallPlayer)
        {
            SmallPlayer currentPlayer = (SmallPlayer)player;
            if (currentPlayer.GetHoldingLight() != null)
            {
                currentPlayer.GetHoldingLight().SetTarget(null);
                currentPlayer.SetHoldingLight(null);
            }
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}

public enum LightSourceColor
{
    White,
    Red,
    Green
}

public static class LightSourceColorConverter
{
    public static Color GetColor(LightSourceColor LScolor)
    {
        switch (LScolor)
        {
            case LightSourceColor.White:
                return Color.white;
            case LightSourceColor.Red:
                return Color.red;
            case LightSourceColor.Green:
                return Color.green;
            default:
                return Color.white;
        }
    }
}