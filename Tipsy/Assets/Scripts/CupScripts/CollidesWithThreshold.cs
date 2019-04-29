using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidesWithThreshold : MonoBehaviour {
    public Rigidbody Prefab;
    public bool useSphere;
    int count = 0;
    public Color liquidColor;


    public void OnParticleCollision(GameObject other)
    {
        count++;
        SpawnLiquid theScript = other.GetComponent<SpawnLiquid>();

        if(other.CompareTag("isCupThreshold"))
        {
            if (theScript != null)
            {
                if(useSphere)
                    theScript.spawnObject(Prefab);
                if (!useSphere)
                    theScript.fillCylinder(Prefab, liquidColor);
            }
            else
            {
                print("Script is null");
            }

        }
        /*else
        {
            print(other.tag);
        }*/

            
    }

}
