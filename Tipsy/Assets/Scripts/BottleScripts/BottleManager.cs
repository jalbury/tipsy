using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleManager : MonoBehaviour 
{
    public float waitTimeUntilDestroy = 5.0f;
    bool emissionState;
    double pourThreshold;
    public ParticleSystem liquidFlow;


    public void release()
    {
        Destroy(gameObject, waitTimeUntilDestroy);
    }

    private void Start()
    {
        emissionState = false;
        // a value between 0 a 1 where 1 is upside down
        pourThreshold = .4;
    }
    private void Update()
    {
        if (!emissionState && isTipped())
        {
            emissionState = true;
            liquidFlow.Play();
        }
        if(emissionState && !isTipped())
        {
            emissionState = false;
            liquidFlow.Stop();
        }
    }

    private bool isTipped()
    {
        double xRot = Mathf.Abs(transform.rotation.x);
        double zRot = Mathf.Abs(transform.rotation.z);
        if ((xRot >= pourThreshold) ||
            (zRot >= pourThreshold))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
