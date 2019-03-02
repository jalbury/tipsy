using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupManager : MonoBehaviour {
    public float maxRayDistance = 500.0f;
    public LayerMask excludeLayers;
    public float speed = 0.5f;
    private Transform mat = null;
    private GameObject barSeat = null;
    private bool canPickMeUp = true;
    private bool onBarSeat = false;
	
	void Update () 
    {
        if (mat != null)
        {
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, mat.position, step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, mat.position) < 0.1f)
            {
                mat = null;
                this.gameObject.GetComponent<Rigidbody>().useGravity = true;
                barSeat.GetComponent<SeatManager>().serve(this.gameObject);
            }
            return;
        }

        // create ray that points directly below cup
        Ray belowCup = new Ray(transform.position, Vector3.up * -1);

        // check if raycast hits a bar seat (meaning we should serve this drink)
        RaycastHit hit;
        if (Physics.Raycast(belowCup, out hit, maxRayDistance, ~excludeLayers))
        {
            if (hit.collider.tag == "barSeat")
            {
                onBarSeat = true;
                barSeat = hit.collider.gameObject;
                barSeat.GetComponent<SeatManager>().pauseTimer();
            }
            else if (hit.collider.tag == "bartenderTable")
            {
                onBarSeat = false;
            }
        }
    }

    // returns whether cup should be thrown
    // cup should not be thrown if it is hovered over bar seat; in that case, it
    // should be served. 
    // otherwise, cup should be thrown otherwise
    public bool release()
    {
        if (onBarSeat)
        {
            // get position to move towards to serve drink
            mat = barSeat.transform.GetChild(0).transform;
            // ensure cup can't be picked up again
            canPickMeUp = false;
            return false;
        }
        return true;
    }

    public bool canPickup()
    {
        return canPickMeUp;
    }

}
