using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOutside : MonoBehaviour {

    public GameObject cameraRig;
    private bool isOutside;
    private RaycastPointer rp = null;

    void OnTriggerExit(Collider other)
    {
        if (GameObject.ReferenceEquals(other.gameObject, cameraRig.GetComponent<RaycastPointer>().pickedUpObject))
        {
            rp = cameraRig.GetComponent<RaycastPointer>();
            rp.objDistance -= 0.1f;
            isOutside = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (GameObject.ReferenceEquals(other.gameObject, cameraRig.GetComponent<RaycastPointer>().pickedUpObject))
        {
            if (rp != null)
            {
                isOutside = false;
                rp = null;
            }
        }
    }

    private void Update()
    {
        if (isOutside && rp != null)
            rp.objDistance -= 0.1f;
    }




}
