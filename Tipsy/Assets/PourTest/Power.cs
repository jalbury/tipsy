using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour {

    public GameObject audioManager;

    public void OnMouseUpAsButton()
    {
        audioManager.GetComponent<AudioManager>().toggleMute();
    }
}
