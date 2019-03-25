using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOutside : MonoBehaviour {

    public GameObject comparisonPoint;

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject == other.transform.parent.Find("OVRCameraRig").GetComponent<RaycastPointer>().pickedUpObject)
            other.transform.parent.Find("OVRCameraRig").GetComponent<RaycastPointer>().objDistance -= (float)0.1;
        //other = (Collider)objectWithin.GetComponent(typeof(MeshFilter));
        /*
        Vector3 direction = comparisonPoint.transform.position - other.transform.position;
        direction.Normalize();
        direction.x *= (float).1;
        direction.y *= (float).1;
        direction.z *= (float).1;
        other.transform.position += direction;
        */
    }


}
