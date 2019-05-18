using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AkiraButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private int button;

    public void OnPointerDown(PointerEventData eventData)
    {
        button = 1;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        button = -1;
        StartCoroutine(ZeroButton());
    }

    private IEnumerator ZeroButton()
    {
        yield return new WaitForEndOfFrame();
        button = 0;
    }

    public int GetButton()
    {
        return button;
    }
}
