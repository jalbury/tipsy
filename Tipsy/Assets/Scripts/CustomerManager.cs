using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour {
    public float waitTime = 3.0f;

	public void serve(GameObject cup)
    {
        Destroy(cup, waitTime);
    }
}
