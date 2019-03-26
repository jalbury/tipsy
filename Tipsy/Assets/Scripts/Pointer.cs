using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
	public float m_DefaultLength = 5.0f;
	public GameObject m_Dot;
	public VRInputMod m_InputModule;
	
	private LineRenderer m_LineRender = null;

	private void Awake()
	{
		m_LineRender = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		UpdateLine();
	}

	private void UpdateLine()
	{
		// Use Default Length or Distance from Input Module
		PointerEventData data = m_InputModule.GetData();
        float targetLength = data.pointerCurrentRaycast.distance <= 0.001f ? m_DefaultLength : data.pointerCurrentRaycast.distance;

		// Raycast
		RaycastHit hit = CreateRaycast(targetLength);

		// Default
		Vector3 endPosition = transform.position + (transform.forward * targetLength);

		// If hit, base it off hit
		//if(hit.collider != null)
			//endPosition = hit.point;

		// Set Position of Dot
		m_Dot.transform.position = endPosition;

		// Set LineRenderer
		m_LineRender.SetPosition(0, transform.position);
		m_LineRender.SetPosition(1, endPosition);
	}

	private RaycastHit CreateRaycast(float length)
	{
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);
		Physics.Raycast(ray, out hit, m_DefaultLength);

		return hit;
	} 
}
