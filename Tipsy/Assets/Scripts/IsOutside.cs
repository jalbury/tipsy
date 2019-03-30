using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOutside : MonoBehaviour {

    public GameObject cameraRig;

    void OnTriggerExit(Collider other)
    {

        if (GameObject.ReferenceEquals(other.gameObject, cameraRig.GetComponent<RaycastPointer>().pickedUpObject))
            cameraRig.GetComponent<RaycastPointer>().objDistance -= (float)0.1;

    }

}
