using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Button : PhysicalButton
{
    public override void onClick()
    {
        base.onClick();
        DataManager.setLevelData(Levels.level3());
        SceneManager.LoadScene("3-Mixed Drinks");
    }
}
