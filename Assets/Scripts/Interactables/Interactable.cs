using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class Interactable : MonoBehaviour
{
    private SphereCollider sphereCollider;

    protected virtual void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            PlayerEnterInteractionZone(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            PlayerExitInteractionZone(player);
        }
    }

    protected void PlayerEnterInteractionZone(PlayerController player)
    {
        player.SetInteractable(this);
    }

    protected void PlayerExitInteractionZone(PlayerController player)
    {
        if (player.GetInteractable() == this)
        {
            player.SetInteractable(null);
        }
    }

    public abstract void InteractX(PlayerController player);
}
