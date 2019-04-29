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
    float liquidAmount;
    int maxParticles;
    private int numFramesSinceFilling;
    public GameObject liquidThreshold;
    private GameObject fillMeter;
    private Text liquidText;
    private Slider slider;
    private float beerMultiplier = 4f;

    //public Rigidbody Prefab;
    int count = 0;
    private Dictionary<string, float> liquids;


    private void Start()
    {
        liquids = new Dictionary<string, float>();
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
        {
            fillMeter.SetActive(false);
            GetComponent<AudioSource>().Stop();

        }
    }

    public void spawnObject(Rigidbody Prefab)
    {
        if (count >= maxSpheres)
            return;

        count++;
        float sphereCount;
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

        var radius = (float)UnityEngine.Random.Range(0, 100) / 1000;
        var rotation = UnityEngine.Random.Range(0, 360);

        Vector3 spot = new Vector3((Spawnpoint.position.x + radius * Mathf.Cos(rotation)),
                        Spawnpoint.position.y,
                        (Spawnpoint.position.z + radius * Mathf.Sin(rotation)));


        RigidPrefab = Instantiate(Prefab, spot,
                    Spawnpoint.rotation) as Rigidbody;
        RigidPrefab.transform.parent = gameObject.transform;
    }

    public void fillCylinder(Rigidbody Prefab, Color liquidColor)
    {
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().pitch = 1 + ((float)liquidAmount / (float)maxParticles);
        fillMeter.SetActive(true);
        numFramesSinceFilling = 0;

        if (liquidAmount >= maxParticles)
        {
            //print("too much");
            return;
        }

        heightPerParticle = heightPerOz * DataManager.ozPerParticle();
        maxParticles = (int)(maxOz / DataManager.ozPerParticle());

        fillMeter.SetActive(true);

        liquidText.text = Prefab.tag;

        float particleMultiplier = 1.5f;
        if (Prefab.tag == "Ale" || Prefab.tag == "Lager" ||
            Prefab.tag == "Malt" || Prefab.tag == "Stout")
        {
            particleMultiplier = beerMultiplier;
        }

        liquidAmount += particleMultiplier;

        if (liquids.ContainsKey(Prefab.tag))
            liquids[Prefab.tag] += particleMultiplier;
        else
            liquids.Add(Prefab.tag, particleMultiplier);

        float curLiqAmount= (float)liquids[Prefab.tag] * DataManager.ozPerParticle();
        // boardMesh.text = Math.Round(curLiqAmount, 2).ToString() + " oz";
        slider.value = curLiqAmount;

        liquidThreshold.transform.localScale += new Vector3(0, particleMultiplier * heightPerParticle,0);
        liquidThreshold.transform.position += new Vector3(0, particleMultiplier * heightPerParticle, 0);

        if (liquidAmount <= 1)
        {
            print("Error");
            liquidThreshold.GetComponent<Renderer>().material.color = liquidColor;
        }
        else
            liquidThreshold.GetComponent<Renderer>().material.color = Color.Lerp(liquidThreshold.GetComponent<Renderer>().material.color,
                liquidColor, (float)particleMultiplier / liquidAmount);
    }
    public Dictionary<string, float> getLiquids()
    {
        return liquids; 
    }
}
