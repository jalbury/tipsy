using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleManager : MonoBehaviour 
{
    public float waitTimeUntilDestroy = 5.0f;

    public void release()
    {
        Destroy(gameObject, waitTimeUntilDestroy);
    }
}
