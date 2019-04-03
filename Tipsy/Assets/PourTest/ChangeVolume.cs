using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVolume : MonoBehaviour {

    public GameObject audioManager;

    [Range(-1.0f,1.0f)]
    public float volumeChange;

    public void OnMouseUpAsButton()
    {
        audioManager.GetComponent<AudioManager>().changeVolume(volumeChange);
    }
}
