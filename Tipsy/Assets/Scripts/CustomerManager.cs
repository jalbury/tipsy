using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public int numberOfCustomers = 6;
    public int secondsBetweenSpawns = 60;
    public GameObject customer = null;
    public GameObject[] seats = null;
    private int customersSpawned = 0;

    void Start()
    {
        // start spawning coroutine
        StartCoroutine(Spawner());
    }

    // spawns customers in randomly-generated intervals
    IEnumerator Spawner()
    {
        // if we've already spawned all customers, end the coroutine
        if (customersSpawned >= numberOfCustomers)
            yield break;

        int wait_time = Random.Range(0, secondsBetweenSpawns);
        yield return new WaitForSeconds(wait_time);

        // try to spawn new customer; if we can't, that's okay too because we're
        // just gonna call this coroutine again
        int seatNum;
        for (int i = 0; i < seats.Length; i++)
        {
            seatNum = Random.Range(0, seats.Length);
            // if seat doesn't already have a customer, add customer there
            if (!seats[seatNum].GetComponent<SeatManager>().hasCustomer())
            {
                seats[seatNum].GetComponent<SeatManager>().addCustomer(customer);
                customersSpawned++;
                break;
            }
        }

        // call Spawner() again to spawn more customers
        StartCoroutine(Spawner());
    }

    void Update()
    {
        // if we haven't spawned all customers yet, do nothing
        if (customersSpawned < numberOfCustomers)
            return;

        // if we've spawned all customers, check if any are still waiting to be served
        for (int i = 0; i < seats.Length; i++)
            if (seats[i].GetComponent<SeatManager>().hasCustomer())
                return;

        // end game if all customers have been served
    }
}