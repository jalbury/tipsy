using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSelectScript : MonoBehaviour {

    public GameObject dispenseObject;

    // returns new instance of prefab saved in dispenseObject
    public GameObject OnSelect() 
    {
        GameObject newObj = Instantiate(dispenseObject);
        return newObj;
    }
}
