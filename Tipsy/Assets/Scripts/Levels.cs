using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Levels {
    public static LevelData level1()
    {
        LevelData levelData;
        levelData.difficultyLevels = new int[6] { 1, 1, 1, 1, 1, 1 };
        levelData.minSpawnTime = 15;
        levelData.maxSpawnTime = 35;
        return levelData;
    }

    public static LevelData level2()
    {
        LevelData levelData;
        levelData.difficultyLevels = new int[8] { 2, 3, 2, 2, 3, 3, 2, 3 };
        levelData.minSpawnTime = 15;
        levelData.maxSpawnTime = 35;
        return levelData;
    }

    public static LevelData level3()
    {
        LevelData levelData;
        levelData.difficultyLevels = new int[8] { 1, 3, 2, 1, 2, 2, 3, 1 };
        levelData.minSpawnTime = 15;
        levelData.maxSpawnTime = 35;
        return levelData;
    }

    public static LevelData level4()
    {
        LevelData levelData;
        levelData.difficultyLevels = new int[7] { 4, 4, 4, 1, 1, 4, 4 };
        levelData.minSpawnTime = 15;
        levelData.maxSpawnTime = 35;
        return levelData;
    }

    public static LevelData level5()
    {
        LevelData levelData;
        levelData.difficultyLevels = new int[10] { 4, 3, 2, 4, 4, 2, 1, 3, 2, 4 };
        levelData.minSpawnTime = 12;
        levelData.maxSpawnTime = 30;
        return levelData;
    }
}
