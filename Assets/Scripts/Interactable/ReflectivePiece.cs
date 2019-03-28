using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectivePiece : Emitter
{
    [SerializeField] private float angleStep = 5;

    protected override void Start()
    {
        base.Start();
        int randomAngleMultiplyer = Random.Range(0, 72);
        SetRayCastAngle(randomAngleMultiplyer * 5 * Time.deltaTime);
    }

    public override void AButtonPressed(PlayerController player)
    {
        SetRayCastAngle(GetRayCastAngle() - angleStep);
    }
}
