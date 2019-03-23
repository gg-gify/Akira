using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPlayer : PlayerController
{
    private LightSource lightSource;

    public void SetLightSource(LightSource lightSource)
    {
        if (this.lightSource != null)
        {
            this.lightSource.gameObject.SetActive(true);
        }
        this.lightSource = lightSource;
        if (lightSource != null)
        {
            lightSource.gameObject.SetActive(false);
            ParticleSystem.MainModule ma = lightSource.GetParticle().main;
            ma.startColor = LightColorConverter.GetColor(lightSource.GetColor());
            lightSource.GetParticle().gameObject.SetActive(true);
        }
        else
        {
            lightSource.GetParticle().gameObject.SetActive(false);
        }
    }

    protected override void CheckInteractionY(bool actionButton)
    {
        if(actionButton && lightSource != null)
        {
            lightSource.gameObject.SetActive(true);
            lightSource = null;
            lightSource.GetParticle().gameObject.SetActive(false);
        }
    }

    public LightSource TransferLightSource()
    {
        LightSource tmp = lightSource;
        lightSource.GetParticle().gameObject.SetActive(false);
        lightSource = null;
        return tmp;
    }

    public LightSource GetLightSource()
    {
        return lightSource;
    }
}
