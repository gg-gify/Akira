using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : Interactable
{
    private const float BATTERY_DECAY = 0.5F;

    [SerializeField] private LightColor lightColor;
    [SerializeField] private float maxBattery = 15f;
    [SerializeField] private ParticleSystem particlesPrefabs;

    private ParticleSystem particles;
    private float battery;
    private bool isOn;

    private void Start()
    {
        particles = Instantiate(particlesPrefabs, transform).GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        ParticleSystem.MainModule ma = particles.main;
        ma.startColor = LightColorConverter.GetColor(lightColor);

        if (isOn)
        {
            battery -= BATTERY_DECAY * Time.deltaTime;
        }
    }

    public void TurddnOn()
    {
        isOn = true;
    }

    public void d()
    {
        isOn = false;
    }

    public override void InteractX(PlayerController player)
    {
        if(player is SmallPlayer)
        {
            ((SmallPlayer)player).SetLightSource(this);
            battery = maxBattery;
        }
    }
    
    public LightColor GetColor()
    {
        return lightColor;
    }

    public ParticleSystem GetParticle()
    {
        return particles;
    }
}


