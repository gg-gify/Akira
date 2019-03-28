using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    private const float MAX_CRYSTAL_BATTERY = .4f;

    [SerializeField] private Light crystalLight;
    // [SerializeField] private MeshRenderer crystalRenderer;

    private bool isRecivingLight;
    private float currentBatteryStatus;
    private Animator crystalAnim;

    private void Start()
    {
        crystalAnim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (isRecivingLight)
        {
            crystalAnim.SetBool("Lit", true);
            currentBatteryStatus -= Time.deltaTime;
            if (currentBatteryStatus <= 0)
            {
                isRecivingLight = false;
            }
        }
        else
        {
            crystalAnim.SetBool("Lit", false);
            crystalLight.enabled = false;
        }
    }

    public void ReciveLight(Color color)
    {
        isRecivingLight = true;
        currentBatteryStatus = MAX_CRYSTAL_BATTERY;
        crystalLight.enabled = true;
        // crystalRenderer.material.color = color;
        crystalLight.color = color;
    }

    public bool IsRecivingLight()
    {
        return isRecivingLight;
    }
}
