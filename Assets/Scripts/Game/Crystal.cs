using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    private const float MAX_CRYSTAL_BATTERY = .4f;

    [SerializeField] Light crystalLight;

    private bool isRecivingLight;
    private float currentBatteryStatus;

    private void Update()
    {
        if (isRecivingLight)
        {
            currentBatteryStatus -= Time.deltaTime;
            if (currentBatteryStatus <= 0)
            {
                isRecivingLight = false;
            }
        }
        else
        {
            crystalLight.enabled = false;
        }
    }

    public void ReciveLight()
    {
        isRecivingLight = true;
        currentBatteryStatus = MAX_CRYSTAL_BATTERY;
        crystalLight.enabled = true;
    }

    public bool IsRecivingLight()
    {
        return isRecivingLight;
    }
}
