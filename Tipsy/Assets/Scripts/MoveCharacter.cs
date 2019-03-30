using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour {

    Animator animator;
    public bool stopWalking = false;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        print("hello");
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(3);
        print("walking");
        animator.Play("Walk");
        yield return new WaitForSeconds(3);
        print("sitting");
        animator.Play("SitDown");
        animator.Play("SittingHandsOnLap");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
