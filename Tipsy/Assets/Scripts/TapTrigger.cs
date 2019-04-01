using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTrigger : MonoBehaviour {
    public string label;
    private TapManager manager;
    private ParticleSystem particleSystem;

    private void Start()
    {
        manager = GetComponentInParent<TapManager>();
        particleSystem = transform.Find("Particle System").GetComponent<ParticleSystem>();
        particleSystem.Stop();
    }

    public void onHover()
    {
        manager.onHover(label);
    }

    public void onClick()
    {
        particleSystem.Play();
    }
}
