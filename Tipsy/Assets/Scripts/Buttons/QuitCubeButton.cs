using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitCubeButton : PhysicalButton 
{
    public override void onClick()
	{
        base.onClick();
		Application.Quit();
	}

}
