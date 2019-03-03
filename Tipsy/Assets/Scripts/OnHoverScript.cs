using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverScript : MonoBehaviour 
{
    public GameObject dropdown = null;
    private bool isShowing = false;

    private void Start()
    {
        dropdown.transform.position -= new Vector3(5, 0, 0);
        isShowing = false;
    }


    public void OnHover()
    {
        if (isShowing)
            return;

        // bring dropdown forward
        dropdown.transform.position += new Vector3(5, 0, 0);

        isShowing = true;
    }

    public void OnUnhover()
    {
        if (!isShowing)
            return;

        // bring dropdown back
        dropdown.transform.position -= new Vector3(5, 0, 0);

        isShowing = false;
    }
}
