using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFallenObjects : MonoBehaviour {
    public float waitTime = 3.0f;

    // Destroy any object tagged "shouldDisappear" that enters the trigger
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "shouldDisappear")
            Destroy(other.gameObject, waitTime);
    }
}
