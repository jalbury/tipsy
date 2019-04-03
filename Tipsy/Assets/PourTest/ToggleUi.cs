using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUi : MonoBehaviour {

    public void OnMouseUpAsButton()
    {
        GameObject audioUI = transform.Find("AudioUI").gameObject;
        audioUI.SetActive(!audioUI.activeSelf);
    }
}
