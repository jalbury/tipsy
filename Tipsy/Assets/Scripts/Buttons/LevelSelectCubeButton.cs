using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectCubeButton : PhysicalButton
{
    public override void onClick()
	{
        base.onClick();
		SceneManager.LoadScene("LevelSelectScene");	
	}
}
