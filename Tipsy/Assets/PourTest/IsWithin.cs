using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsWithin : MonoBehaviour {

    public GameObject comparisonPoint;


   void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == other.transform.parent.Find("OVRCameraRig").GetComponent<RaycastPointer>().pickedUpObject)
            other.transform.parent.Find("OVRCameraRig").GetComponent<RaycastPointer>().objDistance += (float) 0.1;

        /*
        Vector3 direction = comparisonPoint.transform.position - other.transform.position;
        direction.Normalize();
        direction.x *= (float).1;
        direction.y *= (float).1;
        direction.z *= (float).1;
        other.transform.position -= direction;
        */
    }


}
