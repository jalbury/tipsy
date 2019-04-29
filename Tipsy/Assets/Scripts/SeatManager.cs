using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using UnityEngine.UI;

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
    private float beImpatientMultiplier = 0.5f, beImpatientThreshold;
    private bool beImpatient;

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
        customer.GetComponent<MoveAgent>().spawnLocation = location;

        // disable billboard while customer is walking
        customer.transform.Find("NewCustomerOrderUI").gameObject.SetActive(false);

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
        customer.transform.Find("NewCustomerOrderUI").gameObject.SetActive(true);
        arrived = true;
        // set timer
        timeLeft = (float)customerOrder.timeLimit;
        timer = timeLeft;
        // calculate time at which beImpatient should be activated
        beImpatientThreshold = timer * (1 - beImpatientMultiplier);

        string orderName = customerOrder.drinkName;
        customer.transform.Find("NewCustomerOrderUI").Find("Drink Name Text").GetComponent<Text>().text = orderName;
        string timerStr = "" + Math.Round(timeLeft, 1) + " seconds";
        customer.transform.Find("NewCustomerOrderUI").Find("Drink Timer").GetComponent<Text>().text = timerStr;
        float progressBar = timeLeft / timer;
        customer.transform.Find("NewCustomerOrderUI").Find("CustomerTimerBar").GetComponent<Slider>().value = progressBar;
        string orderStr = customerOrder.container;

        foreach (KeyValuePair<string, float> c in customerOrder.contents)
        {
            orderStr += "\n" + c.Value + " oz. " + c.Key;
        }
        customer.transform.Find("NewCustomerOrderUI").Find("Contents").GetComponent<Text>().text = orderStr;

        // hide text that shows added score
        customer.transform.GetChild(1).GetComponent<TextMesh>().text = "";
    }

    private void onCustomerLeave()
    {
        // disable billboard while customer is walking
        customer.transform.Find("NewCustomerOrderUI").gameObject.SetActive(false);

        // hide text that shows added score
        customer.transform.GetChild(1).GetComponent<TextMesh>().text = "";

        served = false;
        pause = false;
        arrived = false;
        beImpatient = false;

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

                if (liquids.Count == customerOrder.contents.Count)
                {
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
                        // make sure accuracy multiplier is not negative
                        accuracyMultiplier = Math.Max(accuracyMultiplier, 0);
                        float timeMultiplier = 1 + (timeLeft / timer);
                        score += (int)(baseScore * timeMultiplier * accuracyMultiplier);
                    }
                }
            }
            else if (customerOrder.container == "Bottle" && customerOrder.contents.ContainsKey(cup.tag))
            {
                float timeMultiplier = 1 + (timeLeft / timer);
                score = (int)(baseScore * timeMultiplier);
            }

            customer.transform.GetChild(1).GetComponent<TextMesh>().text = "+ " + score;
            DataManager.addToScore(score);

            served = true;

            // wait before destroying anything
            yield return new WaitForSeconds(waitTimeUntilDestroy);

            // get rid of customer
            onCustomerLeave();

            // destroy cup
            Destroy(cup);

            yield break;
        }

        // if customer is not here, just destroy cup
        yield return new WaitForSeconds(waitTimeUntilDestroy);
        Destroy(cup);
    }

    // pauses the timer for the current current (if there is one currently)
    public void pauseTimer()
    {
        // set the pause flag to true to indicate that the customer is in
        // the process of being served (the cup is being lowered to the bar mat)
        if (customer != null && arrived)
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

        // if customer has waited past the beImpatient threshold, activate
        // the beImpatient animation
        if (timeLeft < beImpatientThreshold && !beImpatient)
        {
            customer.GetComponent<Animator>().SetBool("beImpatient", true);
            beImpatient = true;
        }

        string timerStr = "" + Math.Round(timeLeft, 0) + " seconds";
        customer.transform.Find("NewCustomerOrderUI").Find("Drink Timer").GetComponent<Text>().text = timerStr;
        float progressBar = timeLeft / timer;
        customer.transform.Find("NewCustomerOrderUI").Find("CustomerTimerBar").GetComponent<Slider>().value = progressBar;
    }

    // if customer is seated, show text if "show" is true and hide text if "show" is false
    public void showBillboardText(bool show)
    {
        if (customer != null && arrived)
            customer.transform.Find("NewCustomerOrderUI").gameObject.SetActive(show);
    }
}
