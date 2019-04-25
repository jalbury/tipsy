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
        levelData.difficultyLevels = new int[8] { 2, 3, 1, 2, 3, 2, 1, 3 };
        levelData.minSpawnTime = 15;
        levelData.maxSpawnTime = 35;
        return levelData;
    }

}
