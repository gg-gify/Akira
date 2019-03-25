using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public abstract class Emitter : Interactable
{
    private const float RAY_MAX_DISTANCE = 20f;

    [SerializeField] private Material beaconPointerMaterial;
    [SerializeField] private float rayOriginOffset = .7f;
    [Range(0, 360)]
    [SerializeField] private float rayCastAngle = 0f;

    protected float currentLineWidth;

    private LineRenderer lineRenderer;
    private Transform beaconPointer;
    private bool isEmittingLight;
    private Color rayColor;
    private ReflectivePiece piece;

    private void Start()
    {
        beaconPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
        beaconPointer.localScale = new Vector3(.3f, .3f, .3f);
        beaconPointer.position = transform.position;
        beaconPointer.SetParent(transform);
        beaconPointer.GetComponent<MeshRenderer>().material = beaconPointerMaterial;
        lineRenderer = GetComponent<LineRenderer>();
        Destroy(beaconPointer.GetComponent<SphereCollider>());
    }

    protected override void Update()
    {
        if (piece != null)
        {
            piece.ShutdownLight();
        }

        beaconPointer.position = CalculateRayOrigin();
        if (isEmittingLight)
        {
            Vector3 startRayPosition = CalculateRayOrigin();
            Vector3 rayDirection = startRayPosition - transform.position;
            rayDirection.Normalize();
            lineRenderer.material.color = rayColor;
            lineRenderer.SetPosition(0, startRayPosition);
            lineRenderer.startWidth = currentLineWidth;
            RaycastHit[] hit = Physics.RaycastAll(startRayPosition, rayDirection);
            Vector3 rayEndPoint = transform.position + rayDirection * RAY_MAX_DISTANCE;
            List<Crystal> possibleLightCrystal = new List<Crystal>();
            if (hit.Length > 0)
            {
                float minorDst = Vector3.Distance(startRayPosition, rayEndPoint);
                for (int i = 0; i < hit.Length; i++)
                {
                    Crystal currentCrystal = hit[i].collider.GetComponent<Crystal>();
                    if (currentCrystal != null)
                    {
                        possibleLightCrystal.Add(currentCrystal);
                    }
                    else
                    {
                        float currentPointDst = Vector3.Distance(startRayPosition, hit[i].point);
                        if (currentPointDst < minorDst)
                        {
                            piece = hit[i].collider.GetComponent<ReflectivePiece>();
                            minorDst = currentPointDst;
                            rayEndPoint = hit[i].point;
                        }
                    }
                }

                for (int i = 0; i < possibleLightCrystal.Count; i++)
                {
                    if (Vector3.Distance(startRayPosition, rayEndPoint) > Vector3.Distance(startRayPosition, possibleLightCrystal[i].transform.position))
                    {
                        possibleLightCrystal[i].ReciveLight(rayColor);
                    }
                }

                if (piece != null)
                {
                    piece.EmitLight(rayColor, currentLineWidth);
                }
                lineRenderer.SetPosition(1, rayEndPoint);
            }
        }
        base.Update();
    }

    public void ShutdownLight()
    {
        lineRenderer.enabled = false;
        isEmittingLight = false;
    }

    public void EmitLight(Color color, float lineWidth)
    {
        lineRenderer.enabled = true;
        isEmittingLight = true;
        rayColor = color;
        currentLineWidth = lineWidth;
    }

    private Vector3 CalculateRayOrigin()
    {
        float currentAngle = rayCastAngle + 90;
        Vector3 rayOrigin = new Vector3(Mathf.Cos(Mathf.Deg2Rad * currentAngle), 0, Mathf.Sin(Mathf.Deg2Rad * currentAngle)) * rayOriginOffset;
        rayOrigin += transform.position;
        return rayOrigin;
    }

    protected bool IsEmittingLight()
    {
        return isEmittingLight;
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = new Color(.2f, .2f, .2f, .8f);

        float currentAngle = rayCastAngle + 90;
        Gizmos.DrawWireSphere(CalculateRayOrigin(), .3f);
    }

}
