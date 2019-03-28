using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] float interactionRadius = 2.3f;
    [SerializeField] Vector3 interactionOffset = Vector3.zero;

    protected virtual void Update()
    {
        CheckPlayer();
    }

    protected void CheckPlayer()
    {
        Vector3 boxInSize = new Vector3(interactionRadius, 1, interactionRadius);
        Vector3 boxOutSize = new Vector3(interactionRadius +1, 1, interactionRadius+1);
        Vector3 boxCenter = transform.position + interactionOffset;
        Collider[] innerhits = Physics.OverlapBox(boxCenter, boxInSize / 2f);
        Collider[] outterHits = Physics.OverlapBox(boxCenter, boxOutSize / 2f);
        for (int i = 0; i < outterHits.Length; i++)
        {
            PlayerController player = outterHits[i].GetComponent<PlayerController>();
            if (player != null)
            {
                bool playerHasExitedInnerBox = true;
                for (int j = 0; j < innerhits.Length; j++)
                {
                    if (outterHits[i] == innerhits[j])
                    {
                        player.SetInteraction(this);
                        playerHasExitedInnerBox = false;
                        j = innerhits.Length;
                    }
                }
                if (playerHasExitedInnerBox)
                {
                    if (player.GetInteraction() == this)
                    {
                        player.SetInteraction(null);
                    }
                }
            }
        }
    }

    public abstract void AButtonPressed(PlayerController player);
    
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(.9f, .8f, 0, .9f);
        Gizmos.DrawWireCube(transform.position + interactionOffset, new Vector3(interactionRadius, 1, interactionRadius));
        Gizmos.color = new Color(.9f, 0, 0, .3f);
        Gizmos.DrawWireCube(transform.position + interactionOffset, new Vector3(interactionRadius + 1, 1, interactionRadius + 1));
    }
}
