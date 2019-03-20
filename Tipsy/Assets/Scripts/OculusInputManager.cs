using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OculusInputManager : BaseInputModule
{
    public Camera cam;
    public OVRInput.Controller targetSource = OVRInput.Controller.RTrackedRemote;
    public OVRInput.Button clickAction = OVRInput.Button.Any;

    private GameObject currentObject = null;
    private PointerEventData data = null;

    protected override void Awake()
    {
        base.Awake();

        data = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        // Reset Data, Set Camera
        data.Reset();
        data.position = new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2);

        // RayCast
        eventSystem.RaycastAll(data, m_RaycastResultCache);
        data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);

        // Clear
        m_RaycastResultCache.Clear();

        // Hover
        HandlePointerExitAndEnter(data, currentObject);

        // Press
        if(OVRInput.GetDown(clickAction, targetSource))
            ProcessPress(data);

        // Release
        if(OVRInput.GetUp(clickAction, targetSource))
            ProcessRelease(data);

    }

    public PointerEventData GetData()
    {
        return data;
    }

    private void ProcessPress(PointerEventData data)
    {
        // Set RayCast
        data.pointerPressRaycast = data.pointerCurrentRaycast;

        // Check for object hit, get the down handle, and call
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(currentObject, data, ExecuteEvents.pointerDownHandler);

        // If no down handler, get click handler
        if(newPointerPress == null)
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

        // Set data
        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = currentObject;

    }

    private void ProcessRelease(PointerEventData data)
    {
        // Execute Pointer Up
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        // Check for Click Handler
        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerUpHandler>(currentObject);

        // Check if actual
        if(data.pointerPress == pointerUpHandler)
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        // Clear selected GameObject
        eventSystem.SetSelectedGameObject(null);

        // Reset Data
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
    }
}
