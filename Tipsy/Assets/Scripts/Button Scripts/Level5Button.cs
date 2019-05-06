using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level5Button : PhysicalButton
{
    public override void onClick()
    {
        base.onClick();
        DataManager.setLevelData(Levels.level5());
        SceneManager.LoadScene("5- Mixed Drinks & Beers");
    }
}
