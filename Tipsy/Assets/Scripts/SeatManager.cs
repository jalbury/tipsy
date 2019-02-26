using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatManager : MonoBehaviour {
    private GameObject customer = null;
    public float waitTimeUntilDestroy = 3.0f;

    // adds customer to this seat
    public void addCustomer(GameObject newCustomer)
    {
        // make sure we don't already have a customer
        if (customer != null)
            return;

        // get location and rotation of customer for this seat
        // Vector3 customerLocation = this.gameObject.transform.GetChild(1).position;
        Transform customerPlacement = this.gameObject.transform.GetChild(1);
        customer = (GameObject)Instantiate(newCustomer, customerPlacement.position, customerPlacement.rotation);
    }

    // returns whether this seat currently has a customer
    public bool hasCustomer()
    {
        return customer != null;
    }

    public void serve(GameObject cup)
    {
        // calculate and display score

        // destroy cup
        Destroy(cup, waitTimeUntilDestroy);

        // destroy customer if there is one
        if (customer != null)
        {
            Destroy(customer, waitTimeUntilDestroy);
            customer = null;
        }
    }
}
