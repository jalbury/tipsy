using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTrigger : MonoBehaviour {
    public string label;
    private TapManager manager;

    private void Start()
    {
        manager = GetComponentInParent<TapManager>();
    }

    public void onHover()
    {
        manager.onHover(label);
    }

    public void onClick()
    {

    }
}
