using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuButton : PhysicalButton 
{
    public override void onClick()
    {
        base.onClick();
        SceneManager.LoadScene("StartMenu");
	}
}

