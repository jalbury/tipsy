﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateAround : MonoBehaviour {

    public bool doorOpensLeft;
    public GameObject pivotPoint;
    public GameObject itemToOpen;
    public float swingSpeed = 100f;
    private float currentAngle;
    private bool closeDoor;
    private bool openDoor;
    private bool isClosed = true;


    private void Update()
    {
        if (closeDoor || openDoor)
        {
            currentAngle += Time.deltaTime * swingSpeed;
            if (currentAngle > 90.0f)
            {
                isClosed = closeDoor;
                closeDoor = false;
                openDoor = false;
                currentAngle = 0f;
                return;
            }

            if ((closeDoor && doorOpensLeft) || (openDoor && !doorOpensLeft))
            {
                itemToOpen.transform.RotateAround(pivotPoint.transform.position, new Vector3(0, 1, 0), -1 * Time.deltaTime * swingSpeed);
            }
            else
            {
                itemToOpen.transform.RotateAround(pivotPoint.transform.position, new Vector3(0, 1, 0), Time.deltaTime * swingSpeed);
            }
        }
    }


    public void OnMouseUpAsButton()
    {
        onClick();
    }

    public void onClick()
    {
        if (openDoor || closeDoor)
            currentAngle = 90f - currentAngle;
        else
            currentAngle = 0f;
        if (isClosed)
        {
            isClosed = false;
            openDoor = true;
            closeDoor = false;
        }
        else
        {
            isClosed = true;
            closeDoor = true;
            openDoor = false;
        }
    }

    IEnumerator autoShut()
    {
        print("This bitch started");
        yield return new WaitForSeconds(10);
        if (!isClosed)
        {
            isClosed = true;
            closeDoor = true;
            openDoor = false;
        }
    }

}
