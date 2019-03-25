using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRInputMod : BaseInputModule
{
	public Camera m_Camera;

	private GameObject m_CurrentObject = null;
	protected PointerEventData m_Data = null;

	protected override void Awake()
	{
		base.Awake();

		m_Data = new PointerEventData(eventSystem);
	}

	public override void Process()
	{
		// Reset Data
		m_Data.Reset();

		// Set Camera
		m_Data.position = new Vector2(m_Camera.pixelWidth / 2, m_Camera.pixelHeight / 2);

		// Raycast
		eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
		m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
		m_CurrentObject = m_Data.pointerCurrentRaycast.gameObject;

		// Clear Raycast
		m_RaycastResultCache.Clear();

		// Hover States
		HandlePointerExitAndEnter(m_Data, m_CurrentObject);
	}

	public PointerEventData GetData()
	{
		return m_Data;
	}

	protected void ProcessPress(PointerEventData data)
	{
		// Set Raycast
		data.pointerPressRaycast = data.pointerCurrentRaycast;

		// Check for object hit, get the Down Handler, Call it
		GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, data, ExecuteEvents.pointerDownHandler);

		// If no Down Handler, Try and get Click Handler
		if(newPointerPress == null)
			newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

		// Set Data
		data.pressPosition = data.position;
		data.pointerPress = newPointerPress;
		data.rawPointerPress = m_CurrentObject;

	}

	protected void ProcessRelease(PointerEventData data)
	{
		// Execute Pointer Up
		ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

		// Check for Click Handler
		GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

		// Check if still on Object
		if(data.pointerPress == pointerUpHandler)
			ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);

		// Clear selected GameObject
		eventSystem.SetSelectedGameObject(null);

		// Reset Data
		data.pressPosition = Vector2.zero;
		data.pointerPress = null;
		data.rawPointerPress = null;
	}
}
