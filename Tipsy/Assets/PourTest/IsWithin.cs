using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsWithin : MonoBehaviour {

    public GameObject comparisonPoint;
    public GameObject cameraRig;


   void OnTriggerStay(Collider other)
    {
        if (GameObject.ReferenceEquals(other.gameObject, cameraRig.GetComponent<RaycastPointer>().pickedUpObject))
        {
            cameraRig.GetComponent<RaycastPointer>().objDistance += (float)0.1;
        }


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
