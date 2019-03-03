using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SeatManager : MonoBehaviour 
{
    private GameObject customer = null;
    private Drink customerOrder;
    public float waitTimeUntilDestroy = 3.0f;
    private float timeLeft;
    private bool served = false;

    // adds customer to this seat
    public void addCustomer(GameObject newCustomer, Drink order)
    {
        // make sure we don't already have a customer
        if (customer != null)
            return;

        // get location of customer for this seat
        Transform customerPlacement = this.gameObject.transform.GetChild(1);
        customer = (GameObject)Instantiate(newCustomer, customerPlacement.position, customerPlacement.rotation);
        customerOrder = order;

        // set timer
        timeLeft = (float)order.timeLimit;

        string orderStr = "Order: " + order.drinkName + "\nContents: ";
        foreach (DrinkContents c in order.contents)
        {
            orderStr += c.amount + " oz. " + c.liquid;
        }
        orderStr += "\nContainer: " + order.container + "\nTimer: " + Math.Round(timeLeft, 2);
        customer.transform.GetChild(0).GetChild(0).GetComponent<TextMesh>().text = orderStr;
    }

    // returns whether this seat currently has a customer
    public bool hasCustomer()
    {
        return customer != null;
    }

    public void serve(GameObject cup)
    {
        // destroy cup
        Destroy(cup, waitTimeUntilDestroy);

        // if there's a customer, calculate and display score and
        // destroy customer object
        if (customer != null)
        {
            DataManager.addToScore(10);
            Destroy(customer, waitTimeUntilDestroy);
            customer = null;
        }

        // reset served flag
        served = false;
    }

    // pauses the timer for the current current (if there is one currently)
    public void pauseTimer()
    {
        if (customer == null)
            return;

        // set the served flag to true to indicate that the customer is in
        // the process of being served (the cup is being lowered to the bar mat)
        served = true;
    }

    private void Update()
    {
        if (customer == null || served)
            return;

        // update timer
        timeLeft -= Time.deltaTime;

        // if the timer runs out and the customer has not been served,
        // destroy the customer object
        if (timeLeft < 0)
        {
            Destroy(customer);
            customer = null;
            served = false;
            return;
        }

        string orderStr = "Order: " + customerOrder.drinkName + "\nContents: ";
        foreach (DrinkContents c in customerOrder.contents)
        {
            orderStr += c.amount + " oz. " + c.liquid;
        }
        orderStr += "\nContainer: " + customerOrder.container + "\nTimer: " + Math.Round(timeLeft, 2);
        customer.transform.GetChild(0).GetChild(0).GetComponent<TextMesh>().text = orderStr;
    }
}
