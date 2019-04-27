using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterDoorway : MonoBehaviour {

    public GameObject pivotPoint;
    public GameObject itemToOpen;
    public float swingSpeed = 90f;
    public int swingAngle;
    float curAngle;

    public void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().Play();

        StartCoroutine("openDoor");
        

    }

   
    IEnumerator openDoor()
    {
        int multiplier = -1;
        curAngle = 0;
        
        while(multiplier!=0)
        {
            multiplier =  curAngle>swingAngle? 1: (multiplier==1 && curAngle<0 ? 0 : multiplier );

            curAngle += Time.deltaTime * swingSpeed* -1 * multiplier;
            itemToOpen.transform.RotateAround(pivotPoint.transform.position, new Vector3(0, 1, 0), multiplier * Time.deltaTime * swingSpeed);
            yield return null;
        }
    }
}
