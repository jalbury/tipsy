using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customerUI : MonoBehaviour
{

	Animator  anim;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	public void onHover()
	{
		anim.SetBool("OffHover", false);
		anim.SetBool("OnHover", true);
	}

	public void offHover()
	{
		anim.SetBool("OnHover", false);
		anim.SetBool("OffHover", true);
	}
}
