using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsWithin : MonoBehaviour {

    public GameObject cameraRig;


   void OnTriggerStay(Collider other)
    {
        if (GameObject.ReferenceEquals(other.gameObject, cameraRig.GetComponent<RaycastPointer>().pickedUpObject))
        {
            cameraRig.GetComponent<RaycastPointer>().objDistance += (float)0.1;
        }
    }
}
