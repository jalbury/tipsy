using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleManager : MonoBehaviour 
{
    public float waitTimeUntilDestroy = 5.0f;
    public float minPourRate = 50f;
    public float maxPourRate = 200f;
    bool emissionState;
    double pourThreshold;
    public ParticleSystem liquidFlow;
    private bool deactivated = false;
    private float timeSinceDropping;


    public void release()
    {
        // deactivate bottle
        deactivated = true;
        timeSinceDropping = 0f;
    }

    public void pickup()
    {
        // re-active bottle so that it is not destroyed
        deactivated = false;
        timeSinceDropping = 0f;
    }

    private void Start()
    {
        emissionState = false;
        // a value between 0 a 1 where 1 is upside down
        pourThreshold = .4;
    }

    private void Update()
    {
        // if bottle is deactivated, turn off particle system flow
        if (deactivated)
        {
            timeSinceDropping += Time.deltaTime;

            if (emissionState)
            {
                emissionState = false;
                liquidFlow.Stop();
            }

            if (timeSinceDropping >= waitTimeUntilDestroy)
                Destroy(gameObject);

            return;
        }

        bool tipped = isTipped();
        if (tipped)
        {
            if (!emissionState)
            {
                emissionState = true;
                liquidFlow.Play();
            }
        }
        else if (emissionState)
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

    private float getPourRate()
    {
        float xRot = Mathf.Abs(transform.rotation.x);
        float zRot = Mathf.Abs(transform.rotation.z);
        float rot = Mathf.Max(xRot, zRot);
        return MapInterval(rot, (float)pourThreshold, 1f, minPourRate, maxPourRate);
    }

    private float MapInterval(float val, float srcMin, float srcMax, float dstMin, float dstMax)
    {
        if (val >= srcMax) return dstMax;
        if (val <= srcMin) return dstMin;
        return dstMin + (val - srcMin) / (srcMax - srcMin) * (dstMax - dstMin);
    }

}
