using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public abstract class Emitter : Interactable
{
    [SerializeField] private Transform lineRenderPivot;
    [SerializeField] private float lineWidth = 0.14f;
    
    private LineRenderer lineRenderer;
    private bool drawLine;
    private Color lineColor;
    private ReflecterPiece piece;

    protected override void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        base.Awake();
    }

    private void Update()
    {
        if(piece != null)
        {
            piece.TurnOffLight();
        }
        if (drawLine)
        {
            lineRenderer.material.color = lineColor;
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, lineRenderPivot.position);
            lineRenderer.widthCurve.keys[0] = new Keyframe(0, lineWidth);
            RaycastHit[] hit = Physics.RaycastAll(lineRenderPivot.position, transform.forward);
            if (hit.Length > 0)
            {
                RaycastHit minorHit = hit[0];
                float minorDst = Vector3.Distance(lineRenderPivot.position, hit[0].point);
                for (int i = 0; i < hit.Length; i++)
                {
                    if (Vector3.Distance(lineRenderPivot.position, hit[i].point) < minorDst)
                    {
                        minorHit = hit[i];
                        minorDst = Vector3.Distance(lineRenderPivot.position, hit[i].point);
                    }
                }
                lineRenderer.SetPosition(1, minorHit.point);
                piece = minorHit.collider.GetComponent<ReflecterPiece>();
                if (piece != null)
                {
                    piece.RecivingLight(lineColor);
                }
            }
            else
            {
                lineRenderer.SetPosition(1, transform.position + transform.forward * 20);
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    protected void DrawLine(Color lineColor)
    {
        this.lineColor = lineColor;
        drawLine = true;
    }

    protected void EraseLine()
    {
        drawLine = false;
    }
}
