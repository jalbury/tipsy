using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : PhysicalButton
{
    public override void onClick()
    {
        base.onClick();
        SceneManager.LoadScene("StartMenu");	
	}
}
