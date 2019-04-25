using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Button : PhysicalButton
{
    public override void onClick()
	{
        base.onClick();
        DataManager.setLevelData(Levels.level1());
        SceneManager.LoadScene("GameScene");	
	}
}
