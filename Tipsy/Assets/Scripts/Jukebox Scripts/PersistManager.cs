using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistManager : MonoBehaviour {
    public GameObject jukebox;
    public Transform jukeboxLocation;

	// Use this for initialization
	void Start () {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Music");

        if (gameObjects.Length == 0)
        {
            Instantiate(jukebox, jukeboxLocation.position, jukeboxLocation.rotation);
        }
        else
        {
            gameObjects[0].transform.parent.transform.position = jukeboxLocation.position;
            gameObjects[0].transform.parent.transform.rotation = jukeboxLocation.rotation;
        }
    }
}
