using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVolume : JukeboxButton {

    public GameObject audioManager;

    [Range(-1.0f,1.0f)]
    public float volumeChange;

    public override void clickAction()
    {
        audioManager.GetComponent<AudioManager>().changeVolume(volumeChange);
    }
}
