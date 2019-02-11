using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastPointer : MonoBehaviour
{
    public Transform leftHandAnchor = null;
    public Transform rightHandAnchor = null;
    public LineRenderer lineRenderer = null;
    public float maxRayDistance = 500.0f;
    public LayerMask excludeLayers;
    public float speedMultiplier = 5.0f;
    private bool onPickupableObject = false;
    private bool canPickupObject = false;
    private bool objectPickedUp = false;
    private GameObject pickedUpObject = null;
    private float pickupDistance;
    private bool throwing = false;
    private Rigidbody rb = null;
    Queue<Vector3> recentPositions = new Queue<Vector3>();

    // initializes anchors and line renderer
    void Awake()
    {
        if (leftHandAnchor == null)
        {
            Debug.LogWarning("Assign LeftHandAnchor in the inspector!");
            GameObject left = GameObject.Find("LeftHandAnchor");
            if (left != null)
            {
                leftHandAnchor = left.transform;
            }
        }
        if (rightHandAnchor == null)
        {
            Debug.LogWarning("Assign RightHandAnchor in the inspector!");
            GameObject right = GameObject.Find("RightHandAnchor");
            if (right != null)
            {
                rightHandAnchor = right.transform;
            }
        }
        if (lineRenderer == null)
        {
            Debug.LogWarning("Assign a line renderer in the inspector!");
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lineRenderer.receiveShadows = false;
            lineRenderer.widthMultiplier = 0.02f;
            lineRenderer.material.color = Color.red;
        }
    }

    // gets transform of the active controller
    Transform Pointer
    {
        get
        {
            OVRInput.Controller controller = OVRInput.GetConnectedControllers();
            if ((controller & OVRInput.Controller.LTrackedRemote) != OVRInput.Controller.None)
            {
                return leftHandAnchor;
            }
            else if ((controller & OVRInput.Controller.RTrackedRemote) != OVRInput.Controller.None)
            {
                return rightHandAnchor;
            }
            return null;
        }
    }

    void Update()
    {
        Transform pointer = Pointer;
        if (pointer == null)
        {
            return;
        }

        // get ray pointing in direction of controller
        Ray laserPointer = new Ray(pointer.position, pointer.forward);

        // handles when object is currently being held
        if (objectPickedUp)
        {
            // drop object when user releases trigger
            if (!(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)))
            {
                dropObject();
                return;
            }

            // store the positions for the last 10 frames
            if (recentPositions.Count >= 10)
                recentPositions.Dequeue();
            recentPositions.Enqueue(rb.position);

            // move picked up object in accordance with the controller's movement
            rb.MovePosition(laserPointer.origin + laserPointer.direction * pickupDistance);
            rb.MoveRotation(pointer.rotation);
            // rb.MoveRotation(Quaternion.Euler(pointer.rotation.eulerAngles.x, pointer.rotation.eulerAngles.y, 180f));
            // rb.MoveRotation(Quaternion.Euler(pointer.localEulerAngles));
            // set angular velocity of object to zero to avoid it spinning in your hand
            rb.angularVelocity = Vector3.zero;

            return;
        }

        // render line to position of laser pointer
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, laserPointer.origin);
            lineRenderer.SetPosition(1, laserPointer.origin + laserPointer.direction * maxRayDistance);
        }

        RaycastHit hit;
        if (Physics.Raycast(laserPointer, out hit, maxRayDistance, ~excludeLayers))
        {
            // render line onto hit object
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(1, hit.point);
            }

            // checks if we hit an object that we are able to pick up
            if (hit.collider.tag == "canPickUp")
            {
                lineRenderer.material.color = Color.blue;

                // if we were holding down the trigger before hovering over object,
                // then we need to release the trigger before being able to pick it up
                if (!onPickupableObject)
                    canPickupObject = !(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger));
                else if (!canPickupObject && !(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)))
                    canPickupObject = true;

                // indicate that we are hovering over object that we can pick up
                onPickupableObject = true;

                // if we can pick up this object and the trigger is down, pick up the object
                if (canPickupObject && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                    pickupObject(hit.collider.gameObject, hit.distance);
            }
            else if (hit.collider.tag == "objectDispenser")
            {
                lineRenderer.material.color = Color.green;

                // if we were holding down the trigger before hovering over object,
                // then we need to release the trigger before being able to pick it up
                if (!onPickupableObject)
                    canPickupObject = !(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger));
                else if (!canPickupObject && !(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)))
                    canPickupObject = true;

                // indicate that we are hovering over object that we can pick up
                onPickupableObject = true;

                // if we can pick up this object and the trigger is down, pick up the object
                if (canPickupObject && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                {
                    GameObject menuItem = hit.collider.gameObject;
                    OnSelectScript oss = menuItem.GetComponent <OnSelectScript>();
                    pickupObject(oss.OnSelect(), hit.distance);
                }
            }
            else if (hit.collider.tag == "hoverable")
            {
                GameObject menuItem = hit.collider.gameObject;
                OnHoverScript ohs = menuItem.GetComponent<OnHoverScript>();
                ohs.OnHover();
            }
            else
            {
                // reset pointer color and indicate that we can't currently pick up an object
                lineRenderer.material.color = Color.red;
                onPickupableObject = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (throwing)
        {
            // calculate velocity of picked up object by calculating difference in position
            // between last 10 frames and multiply by supplied speed multiplier
            rb.velocity = (rb.position - recentPositions.Peek()) * speedMultiplier;

            // clear out recent positions queue since we've dropped the object
            recentPositions.Clear();

            // we're no longer throwing the object
            throwing = false;
        }
    }

    void pickupObject(GameObject obj, float distance)
    {
        // disable visualization of raycaster
        lineRenderer.enabled = false;

        // indicate that object is picked up and store necessary components
        objectPickedUp = true;
        pickedUpObject = obj;
        pickupDistance = distance;

        rb = pickedUpObject.GetComponent<Rigidbody>();
        // disable gravity on picked-up object to avoid object falling while you're holding it
        rb.useGravity = false;
        // set angular velocity of object to zero to avoid it spinning in your hand
        rb.angularVelocity = Vector3.zero;
        throwing = false;
    }

    void dropObject()
    {
        // indicate that object is no longer being held, it is now thrown
        rb.useGravity = true;
        objectPickedUp = false;
        throwing = true;
        lineRenderer.enabled = true;
    }
}
