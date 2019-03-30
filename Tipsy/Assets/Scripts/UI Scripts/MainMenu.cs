using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void Play()
	{
        SceneManager.LoadScene("Game");
		Debug.Log("Play Game");
	}

	public void Quit()
	{
		Application.Quit();
		Debug.Log("Quit");
	}
}
