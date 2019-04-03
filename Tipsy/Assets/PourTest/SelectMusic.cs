using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMusic : MonoBehaviour {

    public AudioClip[] songs;
    public int curSong;

    public GameObject audioManager;

    public void OnMouseUpAsButton()
    {
        audioManager.GetComponent<AudioManager>().setCurrent(this.gameObject);
    }

    public AudioClip nextSong()
    {
        curSong = (curSong + 1) % songs.Length;
        return songs[curSong];
    }
}
