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
	public void onClick()
	{
		this.GetComponent<MeshRenderer>().material = clickMat;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		Debug.Log("Playing Game");
	}

}
