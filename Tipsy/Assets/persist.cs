using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class persist : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);	
	}
	
}
