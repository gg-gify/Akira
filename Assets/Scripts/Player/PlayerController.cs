using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public AkiraInputModel inputModel;
    public float speed = 12F;
    protected Interactable interactable;
    
    private void Update()
    {
        Vector2 axis = AkiraInput.GetMovimentAxis(inputModel);
        bool actionButtonX = AkiraInput.GetButtonX(inputModel);
        bool actionButtonY = AkiraInput.GetButtonY(inputModel);

        CheckInteractionX(actionButtonX);
        CheckInteractionY(actionButtonY);

        axis.Normalize();

        float angle = Vector2.Angle(Vector2.up, axis);
        if (axis.x <= 0) angle = -angle;

        if (axis != Vector2.zero)
            transform.eulerAngles = new Vector3(0, angle, 0);

        Vector3 axis3D = new Vector3(axis.x, 0, axis.y);

        transform.Translate(axis3D * Time.deltaTime * speed, Space.World);
    }

    public void SetInteractable(Interactable interactable)
    {
        this.interactable = interactable;
    }

    public Interactable GetInteractable()
    {
        return interactable;
    }

    protected void CheckInteractionX(bool actionButton)
    {
        if(actionButton && interactable != null)
        {
            interactable.InteractX(this);
        }
    }

    protected abstract void CheckInteractionY(bool actionButton);
}
