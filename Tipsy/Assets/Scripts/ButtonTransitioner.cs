using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTransitioner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
	public Color32 m_NormalColor = Color.white;
	public Color32 m_HoverColor = Color.grey;
	public Color32 m_DownColor = Color.red;

	private Image m_Image = null;

	private void Awake()
	{
		m_Image = GetComponent<Image>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("Enter");
		m_Image.color = m_HoverColor;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("Exit");
		m_Image.color = m_NormalColor;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("Down");
		m_Image.color = m_DownColor;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Debug.Log("Up");
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Click");
		m_Image.color = m_HoverColor;
	}

}
