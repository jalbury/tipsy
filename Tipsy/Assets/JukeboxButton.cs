using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeboxButton : MonoBehaviour {
    public Material defaultMat;
    public Material hoverMat;
    public Material clickMat;
    private int numFramesSinceHovering;
    private int numFramesSinceClicking;
    private MeshRenderer rend;
    private bool clicked;

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        numFramesSinceHovering++;
        if (numFramesSinceHovering >= 5)
            onDefault();

        numFramesSinceClicking++;
        if (numFramesSinceClicking >= 5)
            clicked = false;
    }

    public void onHover()
    {
        numFramesSinceHovering = 0;
        rend.material = hoverMat;
    }

    public void onDefault()
    {
        rend.material = defaultMat;
    }

    public void onClick()
    {
        if (!clicked)
        {
            rend.material = clickMat;
            clickAction();
        }

        numFramesSinceClicking = 0;
        clicked = true;
    }

    public virtual void clickAction()
    {

    }
}
