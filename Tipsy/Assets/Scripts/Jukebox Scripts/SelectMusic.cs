using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMusic : JukeboxButton {

    public AudioClip[] songs;
    public int curSong;

    public GameObject audioManager;

    public override void clickAction()
    {
        audioManager.GetComponent<AudioManager>().setCurrent(this.gameObject);
    }

    public AudioClip nextSong()
    {
        curSong = (curSong + 1) % songs.Length;
        return songs[curSong];
    }

    public AudioClip randomSong()
    {
        curSong = Random.Range(0, songs.Length);
        return songs[curSong];
    }
}
