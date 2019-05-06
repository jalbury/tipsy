using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapManager : MonoBehaviour {
    private TextMesh textMesh;
    private int numFramesSinceHovering;


    void Start()
    { 
        textMesh = transform.Find("Label").GetComponent<TextMesh>();
    }

    public void onHover(string label)
    {
        textMesh.text = label;
        numFramesSinceHovering = 0;
    }

    void Update() 
    {
        if (textMesh.text != "")
        {
            numFramesSinceHovering++;
            if (numFramesSinceHovering >= 5)
                textMesh.text = "";
        }
    }
}
