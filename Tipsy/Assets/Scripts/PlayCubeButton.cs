using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayCubeButton : MonoBehaviour
{
	public Material defaultMat;
	public Material hoverMat;
	public Material clickMat;
    private int numFramesSinceHovering;
    private MeshRenderer rend;

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.enabled = false;
    }

    private void Update()
    {
        numFramesSinceHovering++;
        if (numFramesSinceHovering >= 5)
            onDefault();
    }

    public void onHover()
	{
        numFramesSinceHovering = 0;
        rend.material = hoverMat;
        rend.enabled = true;
	}

	public void onDefault()
	{
        rend.enabled = false;
	}

	public void onClick()
	{
		rend.material = clickMat;
        rend.enabled = true;

        // get data for first level (for multiple levels, this should be added
        // to the onclick for whatever button launches a specific level)
        LevelData levelData;
        levelData.difficultyLevels = new int[6] { 1, 1, 1, 1, 1, 1};
        levelData.timeBetweenSpawns = 30;
        DataManager.setLevelData(levelData);
		SceneManager.LoadScene("GameScene");
	}

}
