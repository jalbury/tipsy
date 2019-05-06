using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterDoorway : MonoBehaviour {

    public GameObject pivotPoint;
    public GameObject itemToOpen;
    public float swingSpeed = 90f;
    public int swingAngle;
    float curAngle;
    const int DONE = 0;
    const int OPENING = -1;
    const int CLOSING = 1;
    const int DOOR_OPEN_TIME = 2;

    public void OnTriggerEnter(Collider other)
    {
        if(curAngle==0)
            StartCoroutine("openDoor");
    }

   
    IEnumerator openDoor()
    {
        GetComponent<AudioSource>().Play();
        int multiplier = OPENING;
        int valueBefore;// = multiplier;
        curAngle = 0;
        
        while(multiplier!=DONE)
        {
            valueBefore = multiplier;
            multiplier =  curAngle>swingAngle ? CLOSING: (multiplier==CLOSING && curAngle<0 ? DONE : multiplier );

            if(valueBefore == OPENING && multiplier!=OPENING)
            {
                yield return new WaitForSeconds(DOOR_OPEN_TIME);
            }

            curAngle += Time.deltaTime * swingSpeed* -1 * multiplier;
            itemToOpen.transform.RotateAround(pivotPoint.transform.position, new Vector3(0, 1, 0), multiplier * Time.deltaTime * swingSpeed);
            yield return null;
        }

        curAngle = 0;
    }
}
