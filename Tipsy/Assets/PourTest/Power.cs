using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : JukeboxButton
{
    public GameObject audioManager;

    public override void clickAction()
    {
        audioManager.GetComponent<AudioManager>().toggleMute();
        TextMesh text = transform.GetChild(0).GetComponent<TextMesh>();
        text.text = (text.text == "TURN\nOFF" ? "TURN\nON" : "TURN\nOFF");
    }
}
