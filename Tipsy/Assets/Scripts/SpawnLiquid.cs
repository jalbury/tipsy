using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLiquid : MonoBehaviour {
    
    public Transform Spawnpoint;
    //public Rigidbody Prefab;
    int count = 0;


    public void spawnObject(Rigidbody Prefab)
    {
        count++;
        print("Instantiation :"+count);

        Rigidbody RigidPrefab;

        var radius = (float)Random.Range(0, 100)/1000;
        var rotation = Random.Range(0, 360);

        Vector3 spot = new Vector3((Spawnpoint.position.x+radius*Mathf.Cos(rotation)),
                        Spawnpoint.position.y,
                        (Spawnpoint.position.z+radius*Mathf.Sin(rotation)));
        

        RigidPrefab = Instantiate(Prefab, spot,
                    Spawnpoint.rotation) as Rigidbody;
        RigidPrefab.transform.parent = gameObject.transform;
    }  

}
