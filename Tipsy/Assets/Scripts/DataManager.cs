using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LevelData
{
    public int minSpawnTime;
    public int maxSpawnTime;
    public int[] difficultyLevels;
}

public static class DataManager
{
    private static int score = 0;
    private static LevelData levelData;
    private static int numSpheresPerOz = 25;
    private static float amountHeightPerOz = .0001f;
    private static float volumePerParticle = .015f;

    // returns the current player score
    public static int getScore()
    {
        return score;
    }

    // adds given score to current player score
    public static void addToScore(int addedScore)
    {
        score += addedScore;
    }

    // returns the level parameters
    public static LevelData getLevelData()
    {
        return levelData;
    }

    // sets the level parameters
    public static void setLevelData(LevelData newLevelData)
    {
        levelData = newLevelData;

        // reset score to 0 just in case it's still saved from last game
        score = 0;
    }

    public static int spheresPerOz()
    {
        return numSpheresPerOz;
    }

    public static float heightPerOz()
    {
        return amountHeightPerOz;
    }


    public static float ozPerParticle()
    {
        return volumePerParticle;
    }
}
