using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour 
{	
    void Update ()
    {
        // get current score from data manager and update text mesh
        this.gameObject.GetComponent<TextMesh>().text = "Score: " + DataManager.getScore().ToString();
    }
}
