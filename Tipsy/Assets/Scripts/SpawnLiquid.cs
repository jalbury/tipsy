using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnLiquid : MonoBehaviour {
    
    public Transform Spawnpoint;
    public int maxSpheres = 150;
    public int spheresPerOz = 25;
    //public Rigidbody Prefab;
    int count = 0;
    private Dictionary<string, int> liquids;

    private void Start()
    {
        liquids = new Dictionary<string, int>();
    }

    public void spawnObject(Rigidbody Prefab)
    {
        if (count >= maxSpheres)
            return;

        count++;
        int sphereCount;
        if (liquids.ContainsKey(Prefab.tag))
        {
            sphereCount = liquids[Prefab.tag] + 1;
            liquids[Prefab.tag] = sphereCount;
        }
        else
        {
            sphereCount = 1;
            liquids.Add(Prefab.tag, sphereCount);
        }

        float volume = (float)sphereCount / spheresPerOz;
        gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMesh>().text = Math.Round(volume, 2).ToString() + " oz";

        Rigidbody RigidPrefab;

        var radius = (float)UnityEngine.Random.Range(0, 100)/1000;
        var rotation = UnityEngine.Random.Range(0, 360);

        Vector3 spot = new Vector3((Spawnpoint.position.x+radius*Mathf.Cos(rotation)),
                        Spawnpoint.position.y,
                        (Spawnpoint.position.z+radius*Mathf.Sin(rotation)));
        

        RigidPrefab = Instantiate(Prefab, spot,
                    Spawnpoint.rotation) as Rigidbody;
        RigidPrefab.transform.parent = gameObject.transform;
    }  

    public Dictionary<string, int> getLiquids()
    {
        return liquids; 
    }
}
