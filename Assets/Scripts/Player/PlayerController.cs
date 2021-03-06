﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    private const float WALK_ANIMATION_TIME = 0.7f;

    [SerializeField] private Transform hammerLightPivot;

    private Interactable interaction;
    private Animator playerAnim;
    private bool isWalking;
    private LightSource holdingLight;
    private MultiplayerAkiraInput input;

    private void Start()
    {
        input = new MultiplayerAkiraInput();
        playerAnim = GetComponentInChildren<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        PadKeyPressed(input.GetPad());
        if (interaction != null && input.GetButtonA() && !isWalking)
        {
            interaction.AButtonPressed(this);
        }
        if (input.GetButtonB() && !isWalking)
        {
            BButtonPressed();
        }
    }

    public void PadKeyPressed(Vector2 pad)
    {
        if (isWalking) return;
        Vector3 worldPos = new Vector3(pad.x, 0, pad.y);

        if (!Physics.CheckBox(transform.position + worldPos, (Vector3.one * 0.9f) / 2f))
        {
            if (pad != Vector2.zero)
            {
                float angle = Vector2.Angle(Vector3.up, pad);
                if (pad.x < 0) angle = -angle;
                transform.eulerAngles = Vector3.up * angle;
                StartCoroutine(WalkAnimation(worldPos));
            }
        }
    }

    public IEnumerator WalkAnimation(Vector3 direction)
    {
        isWalking = true;
        playerAnim.SetTrigger("Walk");
        Vector3 finalPosition = transform.position + direction;
        while (Vector3.Distance(transform.position, finalPosition) > 0.2f)
        {
            transform.Translate(direction * Time.deltaTime / WALK_ANIMATION_TIME, Space.World);
            yield return new WaitForEndOfFrame();
        }
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));
        yield return new WaitForSeconds(0.2f);
        isWalking = false;
    }

    public void BButtonPressed()
    {
        if (holdingLight != null)
        {
            holdingLight.SetTarget(null);
            holdingLight = null;
        }
    }

    public Interactable GetInteraction()
    {
        return interaction;
    }

    public void SetInteraction(Interactable interaction)
    {
        this.interaction = interaction;
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
            holdingLight.SetTarget(hammerLightPivot);
        }
    }

    public MultiplayerAkiraInput GetInput()
    {
        return input;
    }
}
