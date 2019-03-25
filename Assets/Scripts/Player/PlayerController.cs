using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerType playerType = PlayerType.SmallPlayer;
    [SerializeField] AkiraInputModel inputModel = AkiraInputModel.Keyboard;

    private Interactable interaction;

    private void Update()
    {
        PadKeyPressed(AkiraInput.GetPad(inputModel));
        if (interaction != null && AkiraInput.GetButtonA(inputModel))
        {
            interaction.AButtonPressed(this);
        }
        if (AkiraInput.GetButtonB(inputModel))
        {
            BButtonPressed();
        }
    }

    public void PadKeyPressed(Vector2 pad)
    {
        Vector3 worldPos = new Vector3(pad.x, 0, pad.y);

        if (!Physics.CheckBox(transform.position + worldPos, (Vector3.one * 0.9f) / 2f))
        {
            transform.Translate(worldPos, Space.World);
            if (pad != Vector2.zero)
            {
                float angle = Vector2.Angle(Vector3.up, pad);
                if (pad.x < 0) angle = -angle;
                transform.eulerAngles = Vector3.up * angle;
            }
        }
    }

    public abstract void BButtonPressed();

    public Interactable GetInteraction()
    {
        return interaction;
    }

    public void SetInteraction(Interactable interaction)
    {
        this.interaction = interaction;
    }

    public PlayerType GetPlayerType()
    {
        return playerType;
    }
}
