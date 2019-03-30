using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateAround : MonoBehaviour {

    public bool doorOpensLeft;
    public GameObject pivotPoint;
    public GameObject itemToOpen;
    bool isClosed;
    int count;

    private void Start()
    {
        isClosed = true;
        count = 0;
    }
    private void Update()
    {
        if(!isClosed)
        {
            count++;
            if (count % 180 == 0)
            {
                close();
                isClosed = true;
            }
        }
        
    }


    private void OnMouseUpAsButton()
    {
        if(isClosed)
        {
            open();
        }
        else if (!isClosed)
        {
            close();
        }
    }
    void open()
    {
        isClosed = false ;
        if(doorOpensLeft)
            itemToOpen.transform.RotateAround(pivotPoint.transform.position, new Vector3(0, 1, 0), 90);
        else
            itemToOpen.transform.RotateAround(pivotPoint.transform.position, new Vector3(0, 1, 0), -90);
    }
    void close()
    {
        isClosed = true;
        if(doorOpensLeft)
            itemToOpen.transform.RotateAround(pivotPoint.transform.position, new Vector3(0, 1, 0), -90);
        else
            itemToOpen.transform.RotateAround(pivotPoint.transform.position, new Vector3(0, 1, 0), 90);
    }
    IEnumerable autoShut()
    {
        print("This bitch started");
        yield return new WaitForSeconds(5);
        if (!isClosed)
            close();

    }

}
