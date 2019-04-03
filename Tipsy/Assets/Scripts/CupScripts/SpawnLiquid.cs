using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SpawnLiquid : MonoBehaviour {
    
    public Transform Spawnpoint;
    public int maxSpheres = 150;
    public float maxHeight = .055f;
    public float maxOz = 6f;
    public int spheresPerOz = 25;
    private float heightPerOz;
    private float heightPerParticle;
    int liquidAmount;
    int maxParticles;
    private int numFramesSinceFilling;
    public GameObject liquidThreshold;
    private GameObject fillMeter;
    private Text liquidText;
    private Slider slider;

    //public Rigidbody Prefab;
    int count = 0;
    private Dictionary<string, int> liquids;


    private void Start()
    {
        liquids = new Dictionary<string, int>();
        fillMeter = transform.Find("Drink Fill Meter UI").gameObject;
        slider = fillMeter.transform.Find("Slider").GetComponent<Slider>();
        liquidText = fillMeter.transform.Find("UI Name").GetComponent<Text>();
        slider.value = 0f;
        liquidText.text = "";
        liquidAmount = 0;
        heightPerOz = maxHeight / maxOz;
        heightPerParticle = heightPerOz * DataManager.ozPerParticle();
        maxParticles = (int)(maxOz / DataManager.ozPerParticle());
        fillMeter.SetActive(false);
    }

    private void Update()
    { 
        numFramesSinceFilling++;
        if (numFramesSinceFilling >= 40)
            fillMeter.SetActive(false);
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
        fillMeter.SetActive(true);
        numFramesSinceFilling = 0;

        if (liquidAmount >= maxParticles)
        {
            print("too much");
            return;
        }

        fillMeter.SetActive(true);

        liquidText.text = Prefab.tag;

        liquidAmount++;
        if (liquids.ContainsKey(Prefab.tag))
            liquids[Prefab.tag]++;
        else
        {
            liquids.Add(Prefab.tag, 1);
        }

        float curLiqAmount= (float)liquids[Prefab.tag] * DataManager.ozPerParticle();
        // boardMesh.text = Math.Round(curLiqAmount, 2).ToString() + " oz";
        slider.value = curLiqAmount;

        liquidThreshold.transform.localScale += new Vector3(0, heightPerParticle,0);
        liquidThreshold.transform.position += new Vector3(0, heightPerParticle, 0);

        if (liquidAmount <= 1)
        {
            print("Error");
            liquidThreshold.GetComponent<Renderer>().material.color = liquidColor;
        }
        else
            liquidThreshold.GetComponent<Renderer>().material.color = Color.Lerp(liquidThreshold.GetComponent<Renderer>().material.color,
                liquidColor, 1.0f / liquidAmount);
    }
    public Dictionary<string, int> getLiquids()
    {
        return liquids; 
    }
}
