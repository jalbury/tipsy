using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    GameObject curPlaylist;
    public GameObject[] musicOptions;
    bool poweredOn;
    private AudioSource _audioSource;

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    public void Start()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(GetComponent<AudioSource>());
        _audioSource = GetComponent<AudioSource>();
        int musicOption = (int) Random.Range(0, musicOptions.Length);
        poweredOn = false;
        curPlaylist = musicOptions[musicOption];
    }

    public void Update()
    {
        if (!_audioSource.isPlaying && poweredOn)
        {
            _audioSource.clip = curPlaylist.GetComponent<SelectMusic>().nextSong();
            _audioSource.Play();
        }
    }

    public void setCurrent(GameObject selectedList)
    {
        curPlaylist = selectedList;
        _audioSource.clip = curPlaylist.GetComponent<SelectMusic>().randomSong();
    }

    public void toggleMute()
    {
        if(poweredOn)
        {
            _audioSource.Pause();
            poweredOn = false;
        }
        else
        {
            _audioSource.Play();
            poweredOn = true;
        }
    }

    public void changeVolume(float difAmount)
    {
        _audioSource.volume += difAmount;
    }
}
