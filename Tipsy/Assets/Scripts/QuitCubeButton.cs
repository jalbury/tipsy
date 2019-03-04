using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitCubeButton : MonoBehaviour 
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
		Application.Quit();
	}

}
