using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level4Button : PhysicalButton
{
    public override void onClick()
    {
        base.onClick();
        DataManager.setLevelData(Levels.level4());
        SceneManager.LoadScene("4-Mixed Drinks");
    }
}
