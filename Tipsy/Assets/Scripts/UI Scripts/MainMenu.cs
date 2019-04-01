using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	private string firstLevel = "1-Shots";
	private string secondLevel = "2-Beers";
	private string thirdLevel = "3-FillIn";
	private string fourthLevel = "4-FillIn";
	private string fifthLevel = "5-FillIn";

	public void Play()
	{
        SceneManager.LoadScene(firstLevel);
		Debug.Log("Play Game");
	}

	public void Quit()
	{
		Application.Quit();
		Debug.Log("Quit");
	}

	public void PlayLevel01()
	{
        SceneManager.LoadScene(firstLevel);
		Debug.Log("Play First Level");
	}

	public void PlayLevel02()
	{
        SceneManager.LoadScene(secondLevel);
		Debug.Log("Play Second Level");
	}

	public void PlayLevel03()
	{
        SceneManager.LoadScene(thirdLevel);
		Debug.Log("Play Third Level");
	}

	public void PlayLevel04()
	{
        SceneManager.LoadScene(fourthLevel);
		Debug.Log("Play Fourth Level");
	}

	public void PlayLevel05()
	{
        SceneManager.LoadScene(fifthLevel);
		Debug.Log("Play Fifth Level");
	}
}
