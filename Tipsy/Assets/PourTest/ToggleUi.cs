using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUi : MonoBehaviour {
    private int numFramesSinceHovering;
    private GameObject audioUI;

    private void Start()
    {
        audioUI = transform.Find("AudioUI").gameObject;
        audioUI.SetActive(false);
    }

    private void Update()
    {
        numFramesSinceHovering++;
        if (numFramesSinceHovering >= 60)
            audioUI.SetActive(false);
    }

    public void onHover()
    {
        numFramesSinceHovering = 0;
        audioUI.SetActive(true);
    }
}
