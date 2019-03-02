﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LevelData
{
    public int timeBetweenSpawns;
    public int[] difficultyLevels;
}

public static class DataManager
{
    private static int score = 0;
    private static LevelData levelData;

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

}
