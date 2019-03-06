﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupManager : MonoBehaviour {
    public float maxRayDistance = 500.0f;
    public LayerMask excludeLayers;
    public float speed = 0.5f;
    public GameObject tableTop = null;
    private Vector3 target;
    private GameObject hitObj = null;
    private bool canPickMeUp = true;
    private bool onBarSeat = false;
    private float yOffset;
    private Vector3 offset;
    private bool released = false;

    // UNCOMMENT TO DEBUG:
    // private LineRenderer lineRenderer;

    private void Start()
    {
        // UNCOMMENT TO DEBUG:
        //lineRenderer = gameObject.AddComponent<LineRenderer>();
        //lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        //lineRenderer.receiveShadows = false;
        //lineRenderer.widthMultiplier = 0.02f;
        //lineRenderer.material.color = Color.red;

        yOffset = gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.y + 0.025f;
        offset = new Vector3(0, yOffset, 0);

        // turn off volume billboard initially
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    void Update () 
    {
        if (released)
        {
            // move towards target
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            // check if we hit target
            if (Vector3.Distance(transform.position, target) < 0.2f)
            {
                released = false;

                if (onBarSeat)
                {
                    hitObj.GetComponent<SeatManager>().serve(this.gameObject);
                }
                else
                {
                    // turn volume billboard on when put on bartender table
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMesh>().text = "0 oz";
                    canPickMeUp = true;
                }
            }

            return;
        }

        // create ray that points directly below cup
        Ray belowCup = new Ray(transform.position - offset, Vector3.up * -1);


        // UNCOMMENT TO DEBUG:
        //if (lineRenderer != null)
        //{
        //    lineRenderer.SetPosition(0, belowCup.origin);
        //    lineRenderer.SetPosition(1, belowCup.origin + belowCup.direction * maxRayDistance);
        //}


        // check if raycast hits a bar seat (meaning we should serve this drink)
        RaycastHit hit;
        if (Physics.Raycast(belowCup, out hit, maxRayDistance, ~excludeLayers))
        {
            // UNCOMMENT TO DEBUG:
            //if (lineRenderer != null)
            //{
            //    lineRenderer.SetPosition(1, hit.point);
            //}

            if (hit.collider.tag == "barSeat")
            {
                onBarSeat = true;
                hitObj = hit.collider.gameObject;
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
    public void release()
    {
        released = true;
        // ensure cup can't be picked up while it's in motion
        canPickMeUp = false;

        if (onBarSeat)
        {
            // get position to move towards to serve drink
            target = hitObj.transform.GetChild(0).transform.position;
            // pause timer for customer
            hitObj.GetComponent<SeatManager>().pauseTimer();
        }
        else
        {
            // get position to move to for the table: the center of the table w.r.t.
            // depth, the top of the table w.r.t. height, at the cup's current z pos
            Vector3 tablePos = tableTop.transform.position;
            target = new Vector3(tablePos.x, tablePos.y, transform.position.z);
        }
    }

    public bool canPickup()
    {
        // turn off volume billboard when cup is going to be picked up
        gameObject.transform.GetChild(1).gameObject.SetActive(false);

        return canPickMeUp;
    }

}
