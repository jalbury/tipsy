using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct DrinkContents
{
    public string liquid;
    public float amount;
}
[System.Serializable]
public struct Drink
{
    public string drinkName;
    public string container;
    public DrinkContents[] contents;
    public int timeLimit;
    public int difficulty;
}

public struct DrinkOrder
{
    public string drinkName;
    public string container;
    public Dictionary<string, float> contents;
    public int timeLimit;
    public int difficulty;
}

public class CustomerManager : MonoBehaviour
{
    private int numberOfCustomers = 6;
    private int secondsBetweenSpawns = 60;
    public GameObject male = null;
    public GameObject female = null;
    public float waitBeforeEnding = 3.0f; 
    private int numDifficultyLevels;
    public GameObject[] seats = null;
    public Drink[] drinks = null;
    public DrinkOrder[] drinkOrders = null;
    private List<DrinkOrder>[] drinksByDifficultyLevel = null;
    private int customersSpawned = 0;
    private int[] difficultyLevels;

    void Start()
    {
        // get data for this level
        LevelData levelData = DataManager.getLevelData();
        numDifficultyLevels = levelData.difficultyLevels.Max();
        numberOfCustomers = levelData.difficultyLevels.Length;
        secondsBetweenSpawns = levelData.timeBetweenSpawns;
        difficultyLevels = levelData.difficultyLevels;

        int numDrinks = drinks.Length;
        drinkOrders = new DrinkOrder[numDrinks];
        for (int i=0; i<numDrinks; i++)
        {
            drinkOrders[i].drinkName = drinks[i].drinkName;
            drinkOrders[i].container = drinks[i].container;
            drinkOrders[i].timeLimit = drinks[i].timeLimit;
            drinkOrders[i].difficulty = drinks[i].difficulty;

            drinkOrders[i].contents = new Dictionary<string, float>();
            foreach(DrinkContents d in drinks[i].contents)
            {
                drinkOrders[i].contents.Add(d.liquid, d.amount);
            }
        }

        // bucket drinks based on difficult level
        drinksByDifficultyLevel = new List<DrinkOrder>[numDifficultyLevels];
        int levelIndex;
        foreach(DrinkOrder d in drinkOrders)
        {
            // get difficulty "bucket" for this drinks
            levelIndex = d.difficulty - 1;

            // make sure list for this bucket has been created; if not, create it
            if (drinksByDifficultyLevel[levelIndex] == null)
                drinksByDifficultyLevel[levelIndex] = new List<DrinkOrder>();

            // add this drink to appropriate bucket
            drinksByDifficultyLevel[levelIndex].Add(d);
        }

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

        // -- change when difficulty levels are passed to level ----
        int difficulty = difficultyLevels[customersSpawned];
        // randomly choose drink with the given difficulty level
        int drinkIndex = Random.Range(0, drinksByDifficultyLevel[difficulty-1].Count);
        DrinkOrder order = drinksByDifficultyLevel[difficulty-1][drinkIndex];

        // try to spawn new customer; if we can't, that's okay too because we're
        // just gonna call this coroutine again
        int seatNum, coinToss = Random.Range(0,2);
        for (int i = 0; i < seats.Length; i++)
        {
            seatNum = Random.Range(0, seats.Length);
            // if seat doesn't already have a customer, add customer there
            if (!seats[seatNum].GetComponent<SeatManager>().hasCustomer())
            {
                seats[seatNum].GetComponent<SeatManager>().addCustomer(coinToss == 0 ? male : female, order);
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

        StartCoroutine(endLevel());
    }

    // end level after waiting for waitBeforeEnding seconds
    IEnumerator endLevel()
    {
        yield return new WaitForSeconds(waitBeforeEnding);
        SceneManager.LoadScene("ScoreScene");
    }
}