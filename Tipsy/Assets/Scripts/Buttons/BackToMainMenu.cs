using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : PhysicalButton
{
    public override void onClick()
    {
        base.onClick();
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");	
	}
}
