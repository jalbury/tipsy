using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidesWithThreshold : MonoBehaviour {
    public Rigidbody Prefab;
    int count = 0;

    public void OnParticleCollision(GameObject other)
    {
        count++;
        print(count);
        SpawnLiquid theScript = other.GetComponent<SpawnLiquid>();

        if(other.CompareTag("isCupThreshold"))
        {
            print("Collided with threshold");
            if (theScript != null)
            {
                theScript.spawnObject(Prefab);
            }
            else
            {
                print("Script is null");
            }

        }
        else
        {
            print(other.tag);
        }

            
    }

}
