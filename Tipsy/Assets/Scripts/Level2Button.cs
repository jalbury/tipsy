﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Button : MonoBehaviour
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
        LevelData levelData;
        levelData.difficultyLevels = new int[8] { 2, 3, 1, 2, 3, 2, 1, 3 };
        levelData.timeBetweenSpawns = 30;
        DataManager.setLevelData(levelData);
        SceneManager.LoadScene("2-Beers");	
	}
}