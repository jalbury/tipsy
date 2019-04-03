using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeDoorManager : MonoBehaviour {

    public bool doorOpensLeft;
    public GameObject pivotPoint;
    public GameObject itemToOpen;
    public float swingSpeed = 100f;
    public string label;
    private TextMesh textMesh;
    private int numFramesSinceHovering;
    private float currentAngle;
    private bool closeDoor;
    private bool openDoor;
    private bool isClosed = true;
    public GameObject dispenseObject;

    private void Start()
    {
        textMesh = transform.Find("Label").GetComponent<TextMesh>();
    }

    private void Update()
    {
        if (textMesh.text != "")
        {
            numFramesSinceHovering++;
            if (numFramesSinceHovering >= 5)
                textMesh.text = "";
        }

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

    public void onHover()
    {
        if (textMesh.text == "")
            textMesh.text = label;
        numFramesSinceHovering = 0;
    }

    public GameObject onClick()
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
            return null;
        }

        isClosed = true;
        closeDoor = true;
        openDoor = false;
        return (GameObject)Instantiate(dispenseObject);
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
