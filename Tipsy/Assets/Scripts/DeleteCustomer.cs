using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCustomer : MonoBehaviour {
    HashSet<GameObject> customers;

    private void Start()
    {
        customers = new HashSet<GameObject>();
    }


    private void OnTriggerEnter(Collider other)
    {
        // don't destroy customers the first time they enter trigger; destroy the
        // second time (when they're leaving bar)
        GameObject customer = other.gameObject;
        if (customers.Contains(customer))
            Destroy(customer);
        else
            customers.Add(customer);
    }
}
