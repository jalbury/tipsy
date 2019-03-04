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

        // set text for customer order billboard
        string orderStr = "Order: " + order.drinkName + "\nContents: ";
        foreach (DrinkContents c in order.contents)
        {
            orderStr += c.amount + " oz. " + c.liquid;
        }
        orderStr += "\nContainer: " + order.container + "\nTimer: " + Math.Round(timeLeft, 2);
        customer.transform.GetChild(0).GetChild(0).GetComponent<TextMesh>().text = orderStr;

        // hide text that shows added score
        customer.transform.GetChild(1).GetComponent<TextMesh>().text = "";
    }

    // returns whether this seat currently has a customer
    public bool hasCustomer()
    {
        return customer != null;
    }

    public void serve(GameObject cup)
    {
        StartCoroutine(serveHelper(cup));
    }

    IEnumerator serveHelper(GameObject cup)
    {
        // if there's a customer, calculate and display score
        if (customer != null)
        {
            int score = 10;
            customer.transform.GetChild(1).GetComponent<TextMesh>().text = "+ " + score;
            DataManager.addToScore(score);
        }

        // wait before destroying anything
        yield return new WaitForSeconds(waitTimeUntilDestroy);

        // destroy customer object if there is one
        if (customer != null)
        {
            Destroy(customer);
            customer = null;
        }

        // destroy cup
        Destroy(cup);

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
