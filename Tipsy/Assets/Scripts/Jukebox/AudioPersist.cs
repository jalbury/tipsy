using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPersist : MonoBehaviour {
    public Transform jukeboxLocation;
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioManager>().PlayMusic();
        GameObject.FindGameObjectWithTag("jukebox").transform.position = jukeboxLocation.position;
        GameObject.FindGameObjectWithTag("jukebox").transform.rotation = jukeboxLocation.rotation;
    }
}
