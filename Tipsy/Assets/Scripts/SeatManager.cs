using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SeatManager : MonoBehaviour 
{
    private GameObject customer = null;
    private DrinkOrder customerOrder;
    public float waitTimeUntilDestroy = 3.0f;
    private float timer;
    private float timeLeft;
    private bool served = false;
    private bool pause = false;
    public int baseScore = 10;

    // adds customer to this seat
    public void addCustomer(GameObject newCustomer, DrinkOrder order)
    {
        // make sure we don't already have a customer
        if (customer != null)
            return;

        // get location of customer for this seat
        Transform customerPlacement = this.gameObject.transform.GetChild(1);
        customer = (GameObject)Instantiate(newCustomer, customerPlacement.position + new Vector3(0f, 5f, 0f), customerPlacement.rotation);
        customerOrder = order;

        // set timer
        timeLeft = (float)order.timeLimit;
        timer = timeLeft;

        // set text for customer order billboard
        string orderStr = "Order: " + order.drinkName + "\nContents: ";
        foreach (KeyValuePair<string, float> c in order.contents)
        {
            orderStr += c.Value + " oz. " + c.Key;
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
        // if there's a customer and they haven't been served, calculate and display score
        if (customer != null && !served)
        {
            int score = 0;
            Dictionary<string, int> liquids = cup.GetComponent<SpawnLiquid>().getLiquids();

            foreach(KeyValuePair<string, int> entry in liquids)
            {
                if (!customerOrder.contents.ContainsKey(entry.Key))
                {
                    score = 0;
                    break;
                }
                float correctAmt = customerOrder.contents[entry.Key];
                float actualAmt = entry.Value / DataManager.spheresPerOz();
                float accuracyMultiplier = 1 - (Mathf.Abs(correctAmt - actualAmt) / correctAmt);
                float timeMultiplier = 1 + (timeLeft / timer);
                score = (int)(baseScore * timeMultiplier * accuracyMultiplier);
            }
            customer.transform.GetChild(1).GetComponent<TextMesh>().text = "+ " + score;
            DataManager.addToScore(score);
        }

        served = true;

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

        // reset served and pause flags
        served = false;
        pause = false;
    }

    // pauses the timer for the current current (if there is one currently)
    public void pauseTimer()
    {
        if (customer == null)
            return;

        // set the pause flag to true to indicate that the customer is in
        // the process of being served (the cup is being lowered to the bar mat)
        pause = true;
    }

    private void Update()
    {
        if (customer == null || pause)
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
            pause = false;
            return;
        }

        string orderStr = "Order: " + customerOrder.drinkName + "\nContents: ";
        foreach(KeyValuePair<string, float> c in customerOrder.contents)
        {
            orderStr += c.Value + " oz. " + c.Key;
        }
        orderStr += "\nContainer: " + customerOrder.container + "\nTimer: " + Math.Round(timeLeft, 2);
        customer.transform.GetChild(0).GetChild(0).GetComponent<TextMesh>().text = orderStr;
    }
}
