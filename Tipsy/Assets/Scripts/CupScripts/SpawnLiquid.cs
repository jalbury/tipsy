using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SpawnLiquid : MonoBehaviour {
    
    public Transform Spawnpoint;
    public int maxSpheres = 150;
    public float maxLiquid = .08f;
    public int spheresPerOz = 25;

    int liquidAmount;
    int maxVolume;

    public TextMesh boardMesh;
    public GameObject liquidThreshold;

    //public Rigidbody Prefab;
    int count = 0;
    private Dictionary<string, int> liquids;


    private void Start()
    {
        liquids = new Dictionary<string, int>();
        liquidAmount = 0;
        maxVolume = 1000;
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

        float volume = (float)sphereCount / DataManager.spheresPerOz();
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

    public void fillCylinder(Rigidbody Prefab, Color liquidColor)
    {
        if (liquidAmount >= maxVolume)
        {
            print("too much");
            return;

        }

        liquidAmount++;
        if (liquids.ContainsKey(Prefab.tag))
            liquids[Prefab.tag]++;
        else
        {
            liquids.Add(Prefab.tag, 1);
        }

        float curLiqAmount= (float)liquids[Prefab.tag] / DataManager.heightPerOz();
        boardMesh.text = Math.Round(curLiqAmount, 2).ToString() + " oz";

        liquidThreshold.transform.localScale += new Vector3(0, DataManager.heightPerOz(),0);
        liquidThreshold.transform.position += new Vector3(0, DataManager.heightPerOz() / 2.0f, 0);

        if (count == 0)
        {
            liquidThreshold.GetComponent<Renderer>().material.color = liquidColor;

        }
        else
            liquidThreshold.GetComponent<Material>().color = Color.Lerp(liquidThreshold.GetComponent<Material>().color,
                liquidColor, 1.0f / liquidAmount);


    }
    public Dictionary<string, int> getLiquids()
    {
        return liquids; 
    }
}
