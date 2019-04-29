using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        int score = DataManager.getScore();
        string text = score > 65 ? "You win!" : "Try harder!";
        this.gameObject.GetComponent<TextMesh>().text = text;
    }
}
