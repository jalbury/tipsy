using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScore : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		this.gameObject.GetComponent<TextMesh>().text = "Score: " + DataManager.getScore().ToString();
	}
}
