using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : Emitter
{
    private LightSource lightSource;
    
    public override void InteractX(PlayerController player)
    {
        if (player is SmallPlayer)
        {
            SmallPlayer sPlayer = ((SmallPlayer)player);

            if (sPlayer.GetLightSource() != null && lightSource == null)
            {
                lightSource = sPlayer.TransferLightSource();
                ParticleSystem.MainModule ma = lightSource.GetParticle().main;
                ma.startColor = LightColorConverter.GetColor(lightSource.GetColor());
                lightSource.GetParticle().gameObject.SetActive(true);
                DrawLine(LightColorConverter.GetColor(lightSource.GetColor()));
            }
            else if (sPlayer.GetLightSource() == null && lightSource != null)
            {
                sPlayer.SetLightSource(lightSource);
                lightSource.GetParticle().gameObject.SetActive(false);
                lightSource = null;
                EraseLine();
            }
            else if(sPlayer.GetLightSource() != null && lightSource != null)
            {
                LightSource tmp = lightSource;
                lightSource = sPlayer.TransferLightSource();
                sPlayer.SetLightSource(tmp);
                ParticleSystem.MainModule ma = lightSource.GetParticle().main;
                ma.startColor = LightColorConverter.GetColor(lightSource.GetColor());
                lightSource.GetParticle().gameObject.SetActive(true);
                DrawLine(LightColorConverter.GetColor(lightSource.GetColor()));

            }

        }

    }

  
}