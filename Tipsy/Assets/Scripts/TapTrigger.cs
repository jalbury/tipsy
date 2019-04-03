﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTrigger : MonoBehaviour {
    public string label;
    private TapManager manager;
    private ParticleSystem liquidFlow;

    private void Start()
    {
        manager = GetComponentInParent<TapManager>();
        liquidFlow = transform.Find("Particle System").GetComponent<ParticleSystem>();
        liquidFlow.Stop();
    }

    public void onHover()
    {
        manager.onHover(label);
    }

    public void onClick()
    {
        DataManager.ozPerParticle(2f * DataManager.ozPerParticle());
        liquidFlow.Play();
    }

    public void onUnclick()
    {
        DataManager.ozPerParticle(DataManager.ozPerParticle() / 2f);
        liquidFlow.Stop();
    }
}
