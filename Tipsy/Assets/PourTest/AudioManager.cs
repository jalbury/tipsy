using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    GameObject curPlaylist;
    public GameObject[] musicOptions;
    bool poweredOn;

    public void Start()
    {
        int musicOption = (int) Random.Range(0, musicOptions.Length);
        poweredOn = true;
        curPlaylist = musicOptions[musicOption];
    }

    public void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying && poweredOn)
        {
            GetComponent<AudioSource>().clip = curPlaylist.GetComponent<SelectMusic>().nextSong();
            GetComponent<AudioSource>().Play();
            print("NewSong");
        }
    }

    public void setCurrent(GameObject selectedList)
    {
        curPlaylist = selectedList;
        GetComponent<AudioSource>().clip = curPlaylist.GetComponent<SelectMusic>().nextSong();
    }

    public void toggleMute()
    {
        if(poweredOn)
        {
            GetComponent<AudioSource>().Pause();
            poweredOn = false;
        }
        else
        {
            GetComponent<AudioSource>().Play();
            poweredOn = true;
        }
    }

    public void changeVolume(float difAmount)
    {
        GetComponent<AudioSource>().volume += difAmount;
    }
}
