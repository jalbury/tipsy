using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusInputModule : VRInputMod
{
	public OVRInput.Controller source = OVRInput.Controller.RTrackedRemote;
	public OVRInput.Button click = OVRInput.Button.Any;
	public override void Process()
	{
		base.Process();

		// Press
		if(OVRInput.GetDown(click, source))
			ProcessPress(m_Data);

		// Release
		if(OVRInput.GetUp(click, source))
			ProcessRelease(m_Data);

	}
}
