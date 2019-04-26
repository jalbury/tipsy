using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class SeatManager : MonoBehaviour 
{
    private GameObject customer = null;
    private DrinkOrder customerOrder;
    public float waitTimeUntilDestroy = 3.0f;
    private float timer;
    private float timeLeft;
    private bool served = false;
    private bool pause = false;
    private bool arrived = false;
    public int baseScore = 10;

    // adds customer to this seat
    public void addCustomer(GameObject newCustomer, DrinkOrder order, Transform location)
    {
        // make sure we don't already have a customer
        if (customer != null)
            return;

        arrived = false;
        customerOrder = order;

        // get location of customer for this seat
        Transform customerPlacement = this.gameObject.transform.GetChild(1).transform;
        customer = (GameObject)Instantiate(newCustomer, location.position, Quaternion.identity);
        customer.transform.position += new Vector3(3f, 3f, 3f);

        // disable billboard while customer is walking
        customer.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
        customer.transform.GetChild(0).GetChild(0).GetComponent<TextMesh>().text = "";

        // hide text that shows added score
        customer.transform.GetChild(1).GetComponent<TextMesh>().text = "";

        // set customer's destination to this seat
        customer.GetComponent<MoveAgent>().SetDestination(customerPlacement.position);
    }

    public void onCustomerArrive()
    {
        if (customer == null)
            return;

        // enable billboard now that customer has arrived
        customer.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
        arrived = true;
        // set timer
        timeLeft = (float)customerOrder.timeLimit;
        timer = timeLeft;

        // set text for customer order billboard
        string orderStr = "Order: " + customerOrder.drinkName + "\nContents: ";
        foreach (KeyValuePair<string, float> c in customerOrder.contents)
        {
            orderStr += c.Value + " oz. " + c.Key;
        }
        orderStr += "\nContainer: " + customerOrder.container + "\nTimer: " + Math.Round(timeLeft, 2);
        customer.transform.GetChild(0).GetChild(0).GetComponent<TextMesh>().text = orderStr;

        // hide text that shows added score
        customer.transform.GetChild(1).GetComponent<TextMesh>().text = "";
    }

    private void onCustomerLeave()
    {
        // disable billboard while customer is walking
        customer.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
        customer.transform.GetChild(0).GetChild(0).GetComponent<TextMesh>().text = "";

        // hide text that shows added score
        customer.transform.GetChild(1).GetComponent<TextMesh>().text = "";

        served = false;
        pause = false;
        arrived = false;

        StartCoroutine(FinishCustomerLeaveSequence());
    }

    IEnumerator FinishCustomerLeaveSequence()
    {
        customer.GetComponent<Animator>().SetBool("leave", true);
        customer.GetComponent<Animator>().SetBool("stopWalking", false);
        yield return new WaitForSeconds(0.5f);
        customer.GetComponent<Animator>().SetBool("leave", false);
        yield return new WaitForSeconds(0.75f);
        customer.GetComponent<MoveAgent>().GoToSpawnLocation();
        yield return new WaitForSeconds(5);
        customer = null;
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
        if (customer != null && arrived && !served)
        {
            arrived = false;
            int score = 0;

            if (cup.tag == "isCupThreshold")
            {
                Dictionary<string, int> liquids = cup.GetComponent<SpawnLiquid>().getLiquids();

                foreach (KeyValuePair<string, int> entry in liquids)
                {
                    if (!customerOrder.contents.ContainsKey(entry.Key))
                    {
                        score = 0;
                        break;
                    }
                    float correctAmt = customerOrder.contents[entry.Key];
                    float actualAmt = entry.Value * DataManager.ozPerParticle();
                    float accuracyMultiplier = 1 - (Mathf.Abs(correctAmt - actualAmt) / correctAmt);
                    float timeMultiplier = 1 + (timeLeft / timer);
                    score += (int)(baseScore * timeMultiplier * accuracyMultiplier);
                }
            }
            else if (customerOrder.container == "Bottle" && customerOrder.contents.ContainsKey(cup.tag))
            {
                float timeMultiplier = 1 + (timeLeft / timer);
                score = (int)(baseScore * timeMultiplier);
            }

            customer.transform.GetChild(1).GetComponent<TextMesh>().text = "+ " + score;
            DataManager.addToScore(score);
        }

        served = true;

        // wait before destroying anything
        yield return new WaitForSeconds(waitTimeUntilDestroy);

        // get rid of customer object if there is one
        if (customer != null)
        {
            onCustomerLeave();
        }

        // destroy cup
        Destroy(cup);
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
        if (customer == null || pause || !arrived)
            return;

        // update timer
        timeLeft -= Time.deltaTime;

        // if the timer runs out and the customer has not been served,
        // destroy the customer object
        if (timeLeft < 0)
        {
            onCustomerLeave();
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
