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
        levelData.difficultyLevels = new int[7] { 4, 4, 4, 4, 4, 4, 4 };
        levelData.minSpawnTime = 15;
        levelData.maxSpawnTime = 35;
        return levelData;
    }
}
