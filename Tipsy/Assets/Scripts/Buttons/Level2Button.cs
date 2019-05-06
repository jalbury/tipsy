using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Button : PhysicalButton
{
    public override void onClick()
    {
        base.onClick();
        DataManager.setLevelData(Levels.level2());
        SceneManager.LoadScene("2-Beers");	
	}
}
