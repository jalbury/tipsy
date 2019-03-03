using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverScript : MonoBehaviour 
{
    public GameObject dropdown = null;
    private bool isShowing = false;

    private void Start()
    {
        // hide dropdown initially
        dropdown.SetActive(false);
        isShowing = false;
    }


    public void OnHover()
    {
        if (isShowing)
            return;

        // show dropdown 
        dropdown.SetActive(true);

        isShowing = true;
    }

    public void OnUnhover()
    {
        if (!isShowing)
            return;

        // hide dropdwon
        dropdown.SetActive(false);

        isShowing = false;
    }
}
