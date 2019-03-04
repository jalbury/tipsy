using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayCubeButton : MonoBehaviour
{

	public Material defaultMat;
	public Material hoverMat;
	public Material clickMat;

	public void onHover()
	{
		this.GetComponent<MeshRenderer>().material = hoverMat;
	}

	public void onDefault()
	{
		this.GetComponent<MeshRenderer>().material = defaultMat;
	}

	public void onClick()
	{
		this.GetComponent<MeshRenderer>().material = clickMat;

        // get data for first level (for multiple levels, this should be added
        // to the onclick for whatever button launches a specific level)
        LevelData levelData;
        levelData.difficultyLevels = new int[6] { 1, 1, 1, 1, 1, 1};
        levelData.timeBetweenSpawns = 10;
        DataManager.setLevelData(levelData);
		SceneManager.LoadScene("GameScene");
	}

}
