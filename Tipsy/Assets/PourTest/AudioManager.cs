using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    GameObject curPlaylist;
    bool poweredOn;

    public void Update()
    { 
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().clip = curPlaylist.GetComponent<SelectMusic>().nextSong();
        
    }

    public void setCurrent(GameObject selectedList)
    {
        curPlaylist = selectedList;
        GetComponent<AudioSource>().clip = curPlaylist.GetComponent<SelectMusic>().nextSong();
    }
}
